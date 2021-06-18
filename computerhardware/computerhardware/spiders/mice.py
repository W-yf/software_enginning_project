import scrapy

from .. import items


class MiceSpider(scrapy.Spider):
    name = 'mice'
    start_urls = ['https://detail.zol.com.cn/mice//']

    def parse(self, response):
        base_path = 'https://detail.zol.com.cn'
        for item in response.xpath('//*[@id="J_PicMode"]/*/h3/a/@href'):
            # yield {'url': base_path + item.get()}
            yield scrapy.Request(url=base_path + item.get(), callback=self.second_parse)  # 跳转到二级界面

        if (next_url := response.xpath(
                "/html/body/div[@class='wrapper clearfix']/div[@class='content']/div[@class='page-box']/div[@class='pagebar']/a[@class='next']/@href")):
                yield scrapy.Request(url=base_path + next_url.get(), callback=self.parse)  # 转跳到下一页

    def second_parse(self, response):
        base_path = 'https://detail.zol.com.cn'
        for item in response.xpath(
                "/html/body/div[@class='wrapper clearfix'][2]/div[@class='content']/div[@class='section'][1]/div[@class='section-content']/a/@href"):
            yield scrapy.Request(url=base_path + item.get(), callback=self.third_parse)  # 跳转到三级页面

    def third_parse(self, response):  # 浏览器自动添加tbody，不要再xpath中出现tbody
        MC = items.Mice()
        MC['MC_name'] = response.xpath(
        "/html/body/div[@class='product-model page-title clearfix']/h1[@class='product-model__name']/text()").get()
        MC['MC_resolution_ratio'] = response.xpath("//table[2]//tr[2]//td//span//text()").get()
        MC['MC_price'] = response.xpath(
        "/html/body/div[@class='wrapper  clearfix']/div[@class='side']/div[@class='goods-card']/div[@class='goods-card__price']/span/text()").get()
        MC['MC_frequency'] = response.xpath("//table[2]//tr[3]//td//span//text()").get()
        yield MC  # 返回产品参数