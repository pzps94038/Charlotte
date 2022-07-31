using Charlotte.DataBase.DbContextModel;
using Charlotte.Interface.Dashbord;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Charlotte.Helper.Dashbord
{
    public class DashbrodHelper : IDashbordHelper

    {
        public async Task<object> MonthSale()
        {
            DateTime now = DateTime.Now;
            int days = DateTime.DaysInMonth(now.Year, now.Month);
            DateTime firstDay = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            DateTime lastDay = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            using (var db = new CharlotteContext())
            {
                var list = await db.OrderDetail.Where(a => a.CreateDate >= firstDay && a.CreateDate <= lastDay).ToListAsync();
                List<object> dataList = new List<object>();
                for (int i = 1; i <= days; i++)
                {
                    // 當天開始時間
                    DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i);
                    // 當天凌晨11:59:59
                    DateTime endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i).AddDays(1).AddSeconds(-1);
                    var data = new
                    {
                        name = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i).ToString("yyyy-MM-dd"),
                        y = list.Where(a => a.CreateDate >= startTime && a.CreateDate <= endTime).Sum(a => a.ProductAmount)
                    };
                    dataList.Add(data);
                };
                var result = new
                {
                    chart = new { type = "column" },
                    title = new { text = "" },
                    xAxis = new { type = "category" },
                    yAxis = new { title = new { text = "" } },
                    series = new List<object>()
                {
                    new
                    {
                        name = "",
                        colorByPoint = true,
                        data = dataList
                    }
                }
                };
                return result;
            }
        }

        public async Task<object> WeekSale()
        {
            var mondayDate = GetMondayDate();
            var sunDate = GetSunDayDate();
            using (var db = new CharlotteContext())
            {
                // 找出大於禮拜一小於禮拜天
                var datalist = await db.OrderDetail.Where(a=> a.CreateDate >= mondayDate && a.CreateDate <= sunDate).Join(db.ProductInformation, a => a.ProductId, b => b.ProductId, (a, b) => new
                {
                    a.ProductAmount,
                    b.ProductTypeId,
                }).Join(db.ProductType, a => a.ProductTypeId, b => b.ProductTypeId, (a, b) => new
                {
                    a.ProductAmount,
                    b.Type,
                    a.ProductTypeId,
                }).GroupBy(a=> a.ProductTypeId).Select(a=> new 
                {
                    name = a.First().Type,
                    y = a.Sum(a=> a.ProductAmount)
                }).ToListAsync();
                var result = new
                {
                    chart = new { type = "pie" },
                    title = new { text = "" },
                    xAxis = new { type = "category" },
                    yAxis = new { title = new { text = "" } },
                    series = new List<object>()
                {
                    new
                    {
                        name = "",
                        colorByPoint = true,
                        data =  datalist
                    }
                }
                };
                return result;
            }
        }
        public async Task<object> RegisteredMember()
        {
            var mondayDate = GetMondayDate();
            var sunDate = GetSunDayDate();
            var week = new List<string>() { "禮拜一", "禮拜二", "禮拜三", "禮拜四", "禮拜五", "禮拜六", "禮拜日" };
            using (var db = new CharlotteContext())
            {
                var nowDate = mondayDate;
                var userWeekList = await db.UserMain.Where(a => a.CreatedDate >= mondayDate && a.CreatedDate <= sunDate).ToListAsync();
                var dataList = new List<object>();
                int i = 0;
                while (nowDate <= sunDate) 
                {
                    var count = userWeekList.Where(a => a.CreatedDate >= nowDate && a.CreatedDate <= Convert.ToDateTime(nowDate.ToString("yyyy/MM/dd") + " 23:59:59")).Count();
                    dataList.Add(new 
                    {
                        y = count,
                        name = week[i]
                    });
                    nowDate = nowDate.AddDays(1);
                    i++;
                }
                var result = new
                {
                    chart = new { type = "column" },
                    title = new { text = "" },
                    xAxis = new { type = "category" },
                    yAxis = new { title = new { text = "" } },
                    series = new List<object>()
                {
                    new
                    {
                        name = "",
                        colorByPoint = true,
                        data =  dataList,
                        pointStart = 1
                    }
                }
                };
                return result;
            }
               
        }
        private DateTime GetMondayDate() 
        {
            var now = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd") + " 00:00:00");
            int dayOfWeek = (int)now.DayOfWeek - (int)DayOfWeek.Monday;
            if (dayOfWeek == -1)
                dayOfWeek = 6;
            TimeSpan ts = new TimeSpan(dayOfWeek, 0, 0, 0);
            return now.Subtract(ts);
        }

        private DateTime GetSunDayDate() 
        {
            var now = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59");
            int dayOfWeek = (int)now.DayOfWeek - (int)DayOfWeek.Sunday;
            if (dayOfWeek != 0)
                dayOfWeek = 7 - dayOfWeek;
            TimeSpan ts = new TimeSpan(dayOfWeek, 0, 0, 0);
            return now.Subtract(ts);
        }
    }
}
