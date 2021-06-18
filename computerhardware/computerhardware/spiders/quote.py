import scrapy
import pandas as pd
from .. import items
from .spider_rest import *


class QuoteSpider(scrapy.Spider):
    name = 'cpu'
    start_urls = ['https://detail.zol.com.cn/cpu']

    def parse(self, response):
        base_path = r'https://detail.zol.com.cn/'
        for item in response.xpath('//*[@id="J_PicMode"]/*/h3/a/@href'):
            yield scrapy.Request(url=base_path + item.get(), callback=self.second_parse)  # 跳转到二级界面

        if (next_url := response.xpath(
                "/html/body/div[@class='wrapper clearfix']/div[@class='content']/div[@class='page-box']/div[@class='pagebar']/a[@class='next']/@href")):
            if (full_path := base_path + next_url.get()).split('/')[-1].split('.')[0] < '5':  # 只爬取5页内容
                yield scrapy.Request(url=full_path, callback=self.parse)  # 转跳到下一页

    def second_parse(self, response):
        base_path = r'https://detail.zol.com.cn/'
        for item in response.xpath(
                "/html/body/div[@class='wrapper clearfix'][2]/div[@class='content']/div[@class='section'][1]/div[@class='section-content']/a/@href"):
            # yield {'url': base_path + item.get()}
            yield scrapy.Request(url=base_path + item.get(), callback=self.third_parse)  # 跳转到三级页面

    def third_parse(self, response):  # 浏览器自动添加tbody，不要再xpath中出现tbody
        name = response.xpath(
            "/html/body/div[@class='product-model page-title clearfix']/h1[@class='product-model__name']/text()").get()
        count: int = 1
        price = response.xpath("/html/body/div[@class='wrapper  clearfix']/div[@class='side']/div[@class='goods-card']/div[@class='goods-card__price']/span/text()").get()
        dic: dict = {}
        dic['名称'] = name[:-2]
        dic['价格'] = price[1:]
        while table := response.xpath(f'//table[{count}]').get():
            table = pd.read_html(table)[0].T
            table.columns = table.iloc[0, :]
            table = table.iloc[1, 1:]
            table = table.apply(func=lambda x: x[:-2] if x.endswith('纠错') else x)
            dic.update(table.to_dict())
            count += 1
        yield dic

