﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using CleverStocker.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static CleverStocker.Common.CommonStandard;

namespace CleverStocker.Spider.Sina.Tests
{
    [TestClass()]
    public class SinaStockSpiderTests
    {
        [TestMethod()]
        public void GetStockTest()
        {
            string response = @"var hq_str_sh600086=""东方金钰,4.380,4.510,4.490,4.570,4.320,4.480,4.490,49796325,220884386.000,210200,4.480,665700,4.470,203900,4.460,346700,4.450,400100,4.440,8700,4.490,48900,4.500,99619,4.510,41400,4.520,67300,4.530,2019-06-27,11:30:00,00"";";
            var match = SinaStockSpider.QuotaRegex.Match(response);
            Assert.IsTrue(match.Success);

            var stock = new Stock("6000086", Markets.ShangHai);
            Quota quota = SinaStockSpider.ConvertToQuota(match, ref stock);

            Assert.AreEqual("东方金钰", stock.Name);
            Assert.AreEqual(4.380, quota.OpeningPriceToday);
            Assert.AreEqual(4.510, quota.ClosingPriceYesterday);
            Assert.AreEqual(4.490, quota.CurrentPrice);
            Assert.AreEqual(4.570, quota.DayHighPrice);
            Assert.AreEqual(4.320, quota.DayLowPrice);
            Assert.AreEqual(4.480, quota.BiddingPrice);
            Assert.AreEqual(4.490, quota.AuctionPrice);
            Assert.AreEqual(49796325, quota.Count);
            Assert.AreEqual(220884386.000, quota.Amount);

            Assert.AreEqual(210200, quota.BuyStrand1);
            Assert.AreEqual(665700, quota.BuyStrand2);
            Assert.AreEqual(203900, quota.BuyStrand3);
            Assert.AreEqual(346700, quota.BuyStrand4);
            Assert.AreEqual(400100, quota.BuyStrand5);
            Assert.AreEqual(4.480, quota.BuyPrice1);
            Assert.AreEqual(4.470, quota.BuyPrice2);
            Assert.AreEqual(4.460, quota.BuyPrice3);
            Assert.AreEqual(4.450, quota.BuyPrice4);
            Assert.AreEqual(4.440, quota.BuyPrice5);

            Assert.AreEqual(8700, quota.SellStrand1);
            Assert.AreEqual(48900, quota.SellStrand2);
            Assert.AreEqual(99619, quota.SellStrand3);
            Assert.AreEqual(41400, quota.SellStrand4);
            Assert.AreEqual(67300, quota.SellStrand5);
            Assert.AreEqual(4.490, quota.SellPrice1);
            Assert.AreEqual(4.500, quota.SellPrice2);
            Assert.AreEqual(4.510, quota.SellPrice3);
            Assert.AreEqual(4.520, quota.SellPrice4);
            Assert.AreEqual(4.530, quota.SellPrice5);

            Assert.AreEqual(new DateTime(2019, 6, 27, 11, 30, 00), quota.UpdateTime);
        }

        [TestMethod()]
        public void GetStockMarketQuotaTest()
        {
            string response = @"var hq_str_s_sh600086=""东方金钰,4.450,-0.060,-1.33,807121,35918"";";
            var match = SinaStockSpider.MarketQuotaRegex.Match(response);
            Assert.IsTrue(match.Success);

            var stock = new Stock("6000086", Markets.ShangHai);
            MarketQuota marketQuota = SinaStockSpider.ConvertToMarketQuota(match, ref stock);

            Assert.AreEqual("东方金钰", stock.Name);
            Assert.AreEqual(4.450, marketQuota.CurrentPrice);
            Assert.AreEqual(-0.06, marketQuota.FluctuatingRange);
            Assert.AreEqual(-1.33, marketQuota.FluctuatingRate);
            Assert.AreEqual(807121, marketQuota.Count);
            Assert.AreEqual(35918, marketQuota.Amount);
            Assert.AreEqual(stock.UpdateTime, marketQuota.UpdateTime);
            Assert.IsTrue(stock.UpdateTime.Subtract(DateTime.Now) < TimeSpan.FromSeconds(5));
        }

        [TestMethod()]
        public void GetCompanyTest()
        {
            string response = @"var dongmi_info = {""id"":""sh600086"",""stock"":""sh600086"",""rank"":1468,""vote"":0,""ok"":0,""nor"":0,""name"":""\u5b8b\u5b5d\u521a"",""position"":""\u4e1c\u65b9\u91d1\u94b0"",""summary"":""\u5b8b\u5b5d\u521a\uff0c\u7537\uff0c\u6c49\u65cf\uff0c1956\u5e7412\u6708\u51fa\u751f\uff0c\u4e2d\u5171\u515a\u5458\uff0c\u5927\u4e13\u6587\u5316\uff0c\u4f1a\u8ba1\u5e08\u3002\u66fe\u4efb\u89e3\u653e\u519b\u67d0\u90e8\u6218\u58eb\u3001\u6392\u957f\u3001\u526f\u8fde\u957f\u3001\u4f1a\u8ba1\uff0c\u6df1\u5733\u53d1\u5c55\u94f6\u884c\u7f57\u6e56\u5206\u7406\u5904\u4e3b\u4efb\uff0c1988\u5e74\u81f32012\u5e744\u6708\u4efb\u6df1\u5733\u5e02\u57ce\u5e02\u5efa\u8bbe\u5f00\u53d1\uff08\u96c6\u56e2\uff09\u516c\u53f8\u8d22\u52a1\u90e8\u526f\u90e8\u957f\u3001\u90e8\u957f\uff0c\u6df1\u5733\u4e16\u7eaa\u661f\u6e90\u80a1\u4efd\u6709\u9650\u516c\u53f8\u8463..."",""corp_brief"":""    \u516c\u53f8\u7cfb\u7531\u539f\u9102\u5dde\u5e02\u670d\u88c5\u603b\u5382\u7b49\u4e8e1993\u5e744\u67082\u65e5\u5171\u540c\u53d1\u8d77\u8bbe\u7acb,\u4ee5\u5176\u7ecf\u8425\u6027\u51c0\u8d44\u4ea7\u6298\u53d1\u8d77\u4eba\u80a12860\u4e07\u80a1,\u5e76\u5b9a\u5411\u52df\u96c6\u804c\u5de5\u80a1300\u4e07\u80a1,1995\u5e746\u6708\u53ca10\u6708\u5206\u522b\u630910:3\u6bd4\u4f8b\u8fdb\u884c\u914d\u80a1\u548c\u8f6c\u589e\u80a1\u672c,1996\u5e743\u6708\u630910:4.5\u53ca10:1\u6bd4\u4f8b\u8fdb\u884c\u9001\u80a1\u548c\u8f6c\u589e,\u7ecf1997\u5e745\u670822\u65e5\u53d1\u884c\u540e,\u4e0a\u5e02\u65f6\u603b\u80a1\u672c\u8fbe11595.99\u4e07\u80a1,\u5176\u804c\u5de5\u80a1816.075\u4e07\u80a1\u5c06\u4e8e\u516c\u4f17\u80a13000\u4e07\u80a11997\u5e746\u67086\u65e5\u5728\u4e0a\u4ea4\u6240\u4e0a\u5e02\u4ea4\u6613\u671f\u6ee1\u4e09\u5e74\u540e\u4e0a\u5e02\u3002"",""industry"":""\u5bb6\u7528\u8f7b\u5de5"",""pic"":"""",""status"":""0"",""ir_index"":""12"",""research"":""--"",""sharepic"":""http:\/\/n.sinaimg.cn\/finance\/798\/w416h382\/20190611\/0296-hyeztys8769891.png""}";
            var match = SinaStockSpider.CompanyRegex.Match(response);
            Assert.IsTrue(match.Success);

            Company company = SinaStockSpider.ConvertToCompany(match);

            Assert.AreEqual("1468", company.Rank);
            Assert.AreEqual(0, company.Vote);

            Assert.IsTrue(company.Brief.StartsWith("    公司系由"));
            Assert.AreEqual("家用轻工", company.Industry);
            Assert.AreEqual("宋孝刚", company.Name);
            Assert.AreEqual("东方金钰", company.Position);
            Assert.AreEqual("0", company.Status);
            Assert.IsTrue(company.Summary.StartsWith("宋孝刚"));
        }

        [TestMethod()]
        public void GetRecentQuotasTest()
        {
            string response = @"/*<script>location.href='//sina.com';</script>*/
regexflag([{""day"":""2019-06-28 14:10:00"",""open"":""4.450"",""high"":""4.450"",""low"":""4.440"",""close"":""4.440"",""volume"":""638700""},{""day"":""2019-06-28 14:15:00"",""open"":""4.450"",""high"":""4.450"",""low"":""4.410"",""close"":""4.420"",""volume"":""1813900""},{""day"":""2019-06-28 14:20:00"",""open"":""4.430"",""high"":""4.430"",""low"":""4.410"",""close"":""4.430"",""volume"":""425200""}]);";

            var jsonMatch = SinaStockSpider.JsonArrayRegex.Match(response);
            Assert.IsTrue(jsonMatch.Success);

            response = jsonMatch.Groups["JsonArray"].Value;
            Assert.AreEqual(
                @"{""day"":""2019-06-28 14:10:00"",""open"":""4.450"",""high"":""4.450"",""low"":""4.440"",""close"":""4.440"",""volume"":""638700""},{""day"":""2019-06-28 14:15:00"",""open"":""4.450"",""high"":""4.450"",""low"":""4.410"",""close"":""4.420"",""volume"":""1813900""},{""day"":""2019-06-28 14:20:00"",""open"":""4.430"",""high"":""4.430"",""low"":""4.410"",""close"":""4.430"",""volume"":""425200""}",
                response);

            var recentQuotas = SinaStockSpider.RecentQuotaRegex.Matches(response)
                    .Cast<Match>()
                    .Select(match => SinaStockSpider.ConvertToRecentQuota(match))
                    .ToArray();

            Assert.AreEqual(3, recentQuotas.Length);

            var recentQuota = recentQuotas[0];

            Assert.AreEqual(new DateTime(2019, 6, 28, 14, 10, 00), recentQuota.DateTime);
            Assert.AreEqual(4.450, recentQuota.OpenningPrice);
            Assert.AreEqual(4.450, recentQuota.HighestPrice);
            Assert.AreEqual(4.440, recentQuota.LowestPrice);
            Assert.AreEqual(4.440, recentQuota.ClosingPrice);
            Assert.AreEqual(638700, recentQuota.Volume);
        }
    }
}