import scrapy
from .. import items

class MotherboardSpider(scrapy.Spider):
    name = 'motherboard'
    start_urls = ['http://detail.zol.com.cn/motherboard/']

    def parse(self, response):
        base_path = 'http://detail.zol.com.cn'
        for item in response.xpath('//*[@id="J_PicMode"]/*/h3/a/@href'):
            # yield {'url': base_path + item.get()}
            yield scrapy.Request(url=base_path + item.get(), callback=self.second_parse)  # 跳转到二级界面

        if (next_url := response.xpath(
                "/html/body/div[@class='wrapper clearfix']/div[@class='content']/div[@class='page-box']/div[@class='pagebar']/a[@class='next']/@href")):
                yield scrapy.Request(url=base_path + next_url.get(), callback=self.parse)  # 转跳到下一页

    def second_parse(self, response):
        base_path = 'http://detail.zol.com.cn'
        for item in response.xpath(
                "/html/body/div[@class='wrapper clearfix'][2]/div[@class='content']/div[@class='section'][1]/div[@class='section-content']/a/@href"):
            yield scrapy.Request(url=base_path + item.get(), callback=self.third_parse)  # 跳转到三级页面

    def third_parse(self, response):  # 浏览器自动添加tbody，不要再xpath中出现tbody
        MB = items.MotherBoard()
        MB['MB_compatibility'] = response.xpath('//table[2]//tr[2]//td//span/text()').get()
        MB['MB_name'] = response.xpath("/html/body/div[@class='product-model page-title clearfix']/h1[@class='product-model__name']/text()").get()
        MB['MB_price'] = response.xpath("//div[@class='goods-card__price']//span/text()").get()
        MB['MB_chip_suite'] = m if (m:= response.xpath('//table[1]//tr[3]/td//span/a/text()').get()) else response.xpath('//table[1]//tr[3]/td//span//text()').get()
        MB['MB_hd_compatibility'] = response.xpath("//table[4]//tr[3]//td[1]//span//text()").get()
        MB['MB_my_compatibility'] = response.xpath("//table[3]//tr[2]//td[1]//span//text()").get()
        yield MB  # 返回产品参数
