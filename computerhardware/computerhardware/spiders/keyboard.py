import scrapy
from .. import items


class KeyboardSpider(scrapy.Spider):
    name = 'keyboard'
    start_urls = ['https://detail.zol.com.cn/keyboard/chengdu/']

    def parse(self, response):
        base_path = 'https://detail.zol.com.cn'
        for item in response.xpath('//*[@id="J_PicMode"]/*/h3/a/@href'):
        #     yield {'url': base_path + item.get()}
            yield scrapy.Request(url=base_path + item.get(), callback=self.second_parse)  # 跳转到二级界面
        # print(response.xpath("/html/body/div[4]/div[1]/div[9]/div[1]/a[5]/@href").get().split('/')[-1].split('.')[0])
        if (next_url := response.xpath("/html/body/div[4]/div[1]/div[9]/div[1]/a[5]/@href").get()) :
            yield scrapy.Request(url=base_path + next_url, callback=self.parse)  # 转跳到下一页

    def second_parse(self, response):
        base_path = 'https://detail.zol.com.cn'
        for item in response.xpath(
                "/html/body/div[@class='wrapper clearfix'][2]/div[@class='content']/div[@class='section'][1]/div[@class='section-content']/a/@href"):
            yield scrapy.Request(url=base_path + item.get(), callback=self.third_parse)  # 跳转到三级页面

    def third_parse(self, response):  # 浏览器自动添加tbody，不要再xpath中出现tbody
        KB = items.KeyBoard()
        KB['KB_name'] = response.xpath(
          "/html/body/div[@class='product-model page-title clearfix']/h1[@class='product-model__name']/text()").get()
        KB['KB_type'] = response.xpath("//table[2]//tr[2]//td//span//text()").get()
        KB['KB_price'] = response.xpath(
          "/html/body/div[@class='wrapper  clearfix']/div[@class='side']/div[@class='goods-card']/div[@class='goods-card__price']/span/text()").get()
        yield KB  # 返回产品参数
#