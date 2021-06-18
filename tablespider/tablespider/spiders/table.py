import scrapy
import pandas as pd


class TableSpider(scrapy.Spider):
    name = 'table'
    start_urls = ['https://detail.zol.com.cn/vga/']
    category = {'主板': "/motherboard", '显卡': "/vga", '内存': "/memory",
                'CPU': "/cpu", '硬盘': "hard_drives", '机箱': "/case",
                '光驱': "/dvdrw", "显示器": '/lcd', "鼠标": "/mice",
                "键盘": '/keyboard'}
    base_path = "https://detail.zol.com.cn"
    forbid_device = {}

    def parse(self, response):  # 一级页面，选择设备
        for name, path in list(TableSpider.category.items()):
            if name not in TableSpider.forbid_device:  # 禁止列表
                yield scrapy.Request(url=TableSpider.base_path + path, callback=self.second_parse, meta={'name': name})

    def second_parse(self, response):
        base_path = r'https://detail.zol.com.cn/'
        for item in response.xpath('//*[@id="J_PicMode"]/*/h3/a/@href'):
            yield scrapy.Request(url=base_path + item.get(), callback=self.third_parse, meta=response.meta)  # 跳转到二级界面

        if next_url := response.xpath( # 翻页
                "/html/body/div[@class='wrapper clearfix']/div[@class='content']/div[@class='page-box']/div[@class='pagebar']/a[@class='next']/@href").get():
            if next_url.split('/')[-1].split('.')[0] < '5':  # 只爬取5页内容
                yield scrapy.Request(url=base_path + next_url, callback=self.second_parse, meta=response.meta)  # 转跳到下一页

    def third_parse(self, response):
        base_path = r'https://detail.zol.com.cn/'
        for item in response.xpath(
                "/html/body/div[@class='wrapper clearfix'][2]/div[@class='content']/div[@class='section'][1]/div[@class='section-content']/a/@href"):
            # yield {'url': base_path + item.get()}
            yield scrapy.Request(url=base_path + item.get(), callback=self.fourth_parse, meta=response.meta)  # 跳转到三级页面

    def fourth_parse(self, response):  # 四级页面，获取详细参数（浏览器自动添加tbody，不要在xpath中出现tbody)
        name = response.xpath(
            "/html/body/div[@class='product-model page-title clearfix']/h1[@class='product-model__name']/text()").get()
        count: int = 1
        price = response.xpath(
            "/html/body/div[@class='wrapper  clearfix']/div[@class='side']/div[@class='goods-card']/div[@class='goods-card__price']/span/text()").get()
        dic: dict = {}
        dic['名称'] = name[:-2]
        dic['价格'] = price[1:]
        dic['种类'] = response.meta['name']
        while table := response.xpath(f'//table[{count}]').get():
            table = pd.read_html(table)[0].T
            table.columns = table.iloc[0, :]
            table = table.iloc[1, 1:]
            table = table.apply(func=lambda x: x[:-2] if x.endswith('纠错') else x)
            dic.update(table.to_dict())
            count += 1
        yield dic
