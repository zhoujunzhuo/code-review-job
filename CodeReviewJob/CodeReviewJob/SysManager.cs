﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeReviewJob
{
    public class SysManager
    {
        public IEnumerable<UserModel> GetOneUser()
        {
            var table = SqlHelper.GetTable("select emp.*,one.BigGroup,one.IsLeader from one.Employee one join Employee emp on emp.Id=one.UserId where one.JobType=1");
            var users = table.Select().Select(p => new UserModel()
            {
                UserId = p[0].ToString(),
                UserName = p[1].ToString(),
                Email = p[2].ToString(),
                BigGroup = p[3].ToString(),
                IsLeader = Boolean.Parse(p[4].ToString())
            });
            return users;
        }
        public void AddWinningPeoples(List<WinningPeople> list)
        {
            string sql = "";
            foreach (var item in list)
            {
                sql = $"insert into one.CodeReview values ('{item.CodeReviewId}','{item.UpdateCodeId}','{item.ReviewDate}')";
                SqlHelper.ExecuteNonQuerySql(System.Data.CommandType.Text, sql);
            }
        }
        public IEnumerable<WinningPeople> GetWinningPeoples()
        {
            var result = new List<WinningPeople>();
            var table = SqlHelper.GetTable(@"
                        select top 3 
                        code.ReviewerId CodeReviewId,
                        code.FixerId UpdateCodeId,
                        (select Name from Employee where Id=code.ReviewerId) ReviewerName,
                        (select Name from Employee where Id=code.FixerId) FixerName,
                        ReviewDate
                        from one.CodeReview code
                        order by ReviewDate desc
                        ");
            var winningPeoples = table.Select().Select(p => new WinningPeople()
            {
                CodeReviewId = p[0].ToString(),
                UpdateCodeId = p[1].ToString(),
                CodeReviewName = p[2].ToString(),
                UpdateCodeName = p[3].ToString(),
                ReviewDate = DateTime.Parse(p[4].ToString())
            }).ToList();
            result.Add(new WinningPeople() { Department = Department.Internal });
            result.Add(new WinningPeople() { Department = Department.Client });
            result.Add(new WinningPeople() { Department = Department.Affiliate });

            for (int i = 0; i < result.Count; i++)
            {
                var item = result[i];
                var data = winningPeoples[i];
                item.CodeReviewId = data.CodeReviewId;
                item.UpdateCodeId = data.UpdateCodeId;
                item.CodeReviewName = data.CodeReviewName;
                item.UpdateCodeName = data.UpdateCodeName;
                item.ReviewDate = data.ReviewDate.Date;
            }
            return result;
        }
    }
}
