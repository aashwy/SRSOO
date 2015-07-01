﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SRSOO.IDAL;
using SRSOO.Util.Data;
using SRSOO.Util.Extension;

namespace SRSOO.SqlServerDAL
{


    }



    public class StudentDAO : DataBase, IStudent
    {
        //    public void Insert(User user)
        //    {
        //        throw new NotImplementedException();

        //    }

        //    public User GetUser(int id)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public User GetUser(string userName)
        //    {
        //        string sql = "select * from [User] where UserName='{0}'".FormatWith(userName.Trim());
        //        SqlDataReader dr = SqlHelper.ExecuteReader(ConStr, CommandType.Text, sql);
        //        if (dr.HasRows == false) return null;
        //        dr.Read();
        //        User user = new User();
        //        user.UserName = dr["UserName"].ToString();
        //        user.PassWord = dr["Password"].ToString();
        //        dr.Close();
        //        dr.Dispose();
        //        return user;
        //    }
        //}
        public Student GetStudent(string id)
        {
        string sql = "select * from Student where id = '{0}'".FormatWith(id);
        SqlDataReader dr = SqlHelper.ExecuteReader(ConStr,CommandType.Text,sql);
        if (dr.HasRows == false) return null;
        dr.Read();
        var stu = new Student(dr["Name"].ToString(), dr["Id"].ToString(), 
                  dr["Major"].ToString(), dr["Degree"].ToString());
        dr.Close();
        dr.Dispose();
            //访问数据库，获取选课信息
        var attends = new List<Section>();
        string sql1 =@"select * from AttendSection where StudentNumber='0'";
        DataTable attendSec = SqlHelper.ExecuteDataset(ConStr, CommandType.Text, sql1),Tables[0];
        var secDao = new SectionDao();
        foreach(DataRow r in attendSec.Rows)
        {
              attends.Add(secDao.GetSection(r["SectionNumber"]))
        }
        attends.Add(new Section(0,"","",null,"",0));
        stu.Attends = attends;
        return stu;
}


    }
}