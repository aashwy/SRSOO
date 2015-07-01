using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRSOO.BLL;
using SRSOO.Util;
using SRSOO.Util.Extension;
public partial class pages_selectCourse : WebBasePage // pages_selectCourse界面/UI类，WebBasePage基类
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"].ConvertToString() == "LoadSchedule")  //ConvertToString扩展方法：转化成字符串
        {
            var schedule = ScheduleService.LoadSchedule("SP2009");
            var q = from item in schedule.GetSortedSections()
                    select new
                    {
                        id = item.RepresentedCourse.CourseNumber,
                        text = "{0} {1} {2}".FormatWith(item.RepresentedCourse.CourseName, item.TimeOfDay, item.Room)
                    };
            string jsonResult = JSONHelper.ToJson(q.ToList());

            Response.Write(jsonResult);
            Response.End();
        }
        else if (Request.Params["Action"].ConvertToString() == "LoadStudentInfo")
        {
            //User u = Seession["CurrentUser"] as User
            var stu = StudentService.LoadStudentInfo(CurrentUser.RelatedPerson);
            //生成ViewModel
            //匿名对象new
            var stuView = new
            {
                Id = stu.Id,
                Name = stu.Name,
                Attends = stu.Attends
            };
            string jsonResult = JSONHelper.ToJson(stuView);//映射前面的赋值 Id = stu.Id,  Name = stu.Name, Attends = stu.Attends                            
            Response.Write(jsonResult);
            Response.End();
        }
    }
}