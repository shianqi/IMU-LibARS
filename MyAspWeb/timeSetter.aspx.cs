using System;
using System.Collections;
using System.Web.UI.WebControls;
using MyAspWeb.DAL;
using MyAspWeb.Model;
using Rule = MyAspWeb.Model.Rule;

namespace MyAspWeb
{
    public partial class timeSetter : System.Web.UI.Page
    {
        ArrayList arrayList = new ArrayList();
        private string userRules;
        private User user;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = UserDAL.findUserByUsername((string) Session["username"]);
            userRules = user.Rule;
            string[] str = userRules.Split('?');
            for (int i = 0; i < str.Length; i++)
            {
                string userRule = str[i];
                string[] item = userRule.Split(',');
                Rule rule = new Rule(item[0],item[1],item[2]);
                arrayList.Add(rule);
            }

            init();

            for (int i = 0; i < 4; i++)
            {
                DropDownList1.Items.Add((i + 1) + "");
            }

            for (int i = 0; i < 14; i++)
            {
                DropDownList2.Items.Add((i + 1) + "");
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            string id = ((Button) sender).ID;
            string buttonId = id.Split('_')[1];
            arrayList.RemoveAt(int.Parse(buttonId));
            save_Rule();
            init();
            reSetTask();
        }

        private void reSetTask()
        {
            TaskQueue.RemoveUserAllTask(user);
            TaskQueue.PushTastToQueue(TaskQueue.GetUserNextTask(user));
        }

        private void save_Rule()
        {
            string ruleStr = "";
            for (int i = 0; i < arrayList.Count; i++)
            {
                Rule rule = (Rule) arrayList[i];
                if (i == 0)
                {
                    ruleStr = rule.StartTime + "," + rule.OrderTime + "," + rule.LoopTime;
                }
                else
                {
                    ruleStr += "?" + rule.StartTime + "," + rule.OrderTime + "," + rule.LoopTime;
                }
            }
            UserDAL.modifyUserRule(user, ruleStr);
        }

        private void init()
        {
            MyTable.Rows.Clear();

            TableHeaderRow tableHeaderRow = new TableHeaderRow();
            TableHeaderCell tableHeaderCell1 = new TableHeaderCell();
            TableHeaderCell tableHeaderCell2 = new TableHeaderCell();
            TableHeaderCell tableHeaderCell3 = new TableHeaderCell();
            TableHeaderCell tableHeaderCell4 = new TableHeaderCell();
            TableHeaderCell tableHeaderCell5 = new TableHeaderCell();

            tableHeaderCell1.Text = "#";
            tableHeaderCell2.Text = "起始时间";
            tableHeaderCell3.Text = "预约时长";
            tableHeaderCell4.Text = "循环间隔";
            tableHeaderCell5.Text = "操作";

            tableHeaderRow.Cells.Add(tableHeaderCell1);
            tableHeaderRow.Cells.Add(tableHeaderCell2);
            tableHeaderRow.Cells.Add(tableHeaderCell3);
            tableHeaderRow.Cells.Add(tableHeaderCell4);
            tableHeaderRow.Cells.Add(tableHeaderCell5);

            MyTable.Rows.Add(tableHeaderRow);

            for (int i = 0; i < arrayList.Count; i++)
            {
                Rule rule = (Rule)arrayList[i];

                TableCell indexCell = new TableCell();
                indexCell.Text = (i + 1) + "";

                TableCell startTimeCell = new TableCell();
                startTimeCell.Text = rule.StartTime;

                TableCell orderTimeCell = new TableCell();
                orderTimeCell.Text = rule.OrderTime;

                TableCell loopCell = new TableCell();
                loopCell.Text = rule.LoopTime;

                TableCell loopCell2 = new TableCell();
                Button btn = new Button();
                btn.ID = "del_"+ i;
                btn.Text = "删除";
                btn.CssClass = "btn btn-primary";
                btn.Click += new EventHandler(btn_Click);
                loopCell2.Controls.Add(btn);

                TableRow tableRow = new TableRow();
                tableRow.Cells.Add(indexCell);
                tableRow.Cells.Add(startTimeCell);
                tableRow.Cells.Add(orderTimeCell);
                tableRow.Cells.Add(loopCell);
                tableRow.Cells.Add(loopCell2);
                tableRow.ID = (i + 1) + "";

                MyTable.Rows.Add(tableRow);
            }
        }

        protected void Add(object sender, EventArgs e)
        {
            userRules = userRules + "?" + dtp_input1.Text + "," + DropDownList1.Text + "," + DropDownList2.Text;
            UserDAL.modifyUserRule(user, userRules);

            arrayList.Add(new Rule(dtp_input1.Text, DropDownList1.Text, DropDownList2.Text));
            init();
            reSetTask();
        }
    }
}