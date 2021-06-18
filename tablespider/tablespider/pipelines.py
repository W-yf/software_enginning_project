# Define your item pipelines here
#
# Don't forget to add your pipeline to the ITEM_PIPELINES setting
# See: https://docs.scrapy.org/en/latest/topics/item-pipeline.html


# useful for handling different item types with a single interface
from itemadapter import ItemAdapter
import pymongo


class TablespiderPipeline:
    def __init__(self):
        self.MyClient = pymongo.MongoClient('localhost')
        self.my_db = self.MyClient['computer']
        self.availability_devices: list = ['主板', '显卡', '内存', 'CPU', '硬盘', '机箱', '光驱', '显示器', '鼠标', '键盘']

    def process_item(self, item, spider):
        item = {i.replace('.', '-'):j for i, j in item.items()} # Mongodb 不支持 "."
        if (spices := item.get('种类')) in self.availability_devices:
            my_col = self.my_db[spices]
            if my_col.find({'名称': item.get('名称')}).count():
                my_col.replace_one({'名称': item.get('名称')}, item)
            else:
                my_col.insert_one(item)
        return item
