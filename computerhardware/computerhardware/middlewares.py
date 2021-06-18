# Define here the models for your spider middleware
#
# See documentation in:
# https://docs.scrapy.org/en/latest/topics/spider-middleware.html
import random

from scrapy import signals
from scrapy.http.headers import Headers
# useful for handling different item types with a single interface
from itemadapter import is_item, ItemAdapter


class ComputerhardwareSpiderMiddleware:
    # Not all methods need to be defined. If a method is not defined,
    # scrapy acts as if the spider middleware does not modify the
    # passed objects.

    @classmethod
    def from_crawler(cls, crawler):
        # This method is used by Scrapy to create your spiders.
        s = cls()
        crawler.signals.connect(s.spider_opened, signal=signals.spider_opened)
        return s

    def process_spider_input(self, response, spider):
        # Called for each response that goes through the spider
        # middleware and into the spider.

        # Should return None or raise an exception.
        return None

    def process_spider_output(self, response, result, spider):
        # Called with the results returned from the Spider, after
        # it has processed the response.

        # Must return an iterable of Request, or item objects.
        for i in result:
            yield i

    def process_spider_exception(self, response, exception, spider):
        # Called when a spider or process_spider_input() method
        # (from other spider middleware) raises an exception.

        # Should return either None or an iterable of Request or item objects.
        pass

    def process_start_requests(self, start_requests, spider):
        # Called with the start requests of the spider, and works
        # similarly to the process_spider_output() method, except
        # that it doesn’t have a response associated.

        # Must return only requests (not items).
        for r in start_requests:
            yield r

    def spider_opened(self, spider):
        spider.logger.info('Spider opened: %s' % spider.name)


class ComputerhardwareDownloaderMiddleware:
    # Not all methods need to be defined. If a method is not defined,
    # scrapy acts as if the downloader middleware does not modify the
    # passed objects.

    @classmethod
    def from_crawler(cls, crawler):
        # This method is used by Scrapy to create your spiders.
        s = cls()
        crawler.signals.connect(s.spider_opened, signal=signals.spider_opened)
        return s

    def process_request(self, request, spider):
        request.headers = Headers({
            'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8',
            'Accept-Language': 'en',
            'authority': 'detail.zol.com.cn',
            'method': 'GET',
            'path': '/cpu/index1354876.shtml',
            'scheme': 'https',
            'accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9',
            'accept-encoding': 'gzip, deflate, br',
            'accept-language': ' zh-CN, zh;q = 0.9',
            'cache-control': 'no-cache',
            'user-agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.106 Safari/56'
        })
        #
        # request.cookies = {'ip_ck': '5MCJ7vv+j7QuMzc4OTE5LjE1OTI3ODgzNjY%3D',
        #                    '__gads':
        #                        {'ID': 'fc1dfc70ac39e146',
        #                         'T': '1592788368',
        #                         'S': 'ALNI_MaZGJ8-tFLvEXtuA3kehj-vffYZAQ'
        #                         },
        #                    'z_pro_city': 's_provice%3Dsichuan%26s_city%3Dchengdu',
        #                    ' Hm_lvt_ae5edc2bc4fc71370807f6187f0a2dd0': '1623828581', ' Adshow': '2',
        #                    ' Hm_lpvt_ae5edc2bc4fc71370807f6187f0a2dd0': '1623832971', ' userProvinceId': '17',
        #                    ' userCityId': '386', ' userCountyId': '0', ' userLocationId': '21', ' realLocationId': '21',
        #                    ' userFidLocationId': '21', ' listSubcateId': '28',
        #                    ' z_day': 'izol145002%3D2%26izol145154%3D5%26izol112296%3D4%26izol112788%3D5%26izol144999%3D1%26izol145153%3D1%26izol145152%3D2%26izol145001%3D1%26ixgo20%3D1%26rdetail%3D9',
        #                    ' lv': '1623836132', ' vn': '8', ' visited_subcateProId': '28-1296984%2C1354876',
        #                    ' questionnaire_pv': '1623801655'
        #                    }
        # request.headers[
        #     'user-agent'] = 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.106 Safari/537.36'
        return None

    def process_response(self, request, response, spider):
        # Called with the response returned from the downloader.

        # Must either;
        # - return a Response object
        # - return a Request object
        # - or raise IgnoreRequest
        return response

    def process_exception(self, request, exception, spider):
        # Called when a download handler or a process_request()
        # (from other downloader middleware) raises an exception.

        # Must either:
        # - return None: continue processing this exception
        # - return a Response object: stops process_exception() chain
        # - return a Request object: stops process_exception() chain
        pass

    def spider_opened(self, spider):
        spider.logger.info('Spider opened: %s' % spider.name)
