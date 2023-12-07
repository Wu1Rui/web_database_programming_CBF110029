using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SimpleWordTest_comb_t : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            Session["DDL_page"] = 0; // x 10
            CBF110029_cambridge.Text = "";
            CBF110029_MV1.ActiveViewIndex = 0;
            CBF110029_input.Text = "";
            CBF110029_nextQBtn.Text = "下一題";
            HyperLink1.Visible = false;
            CBF110029_input.Visible = true;
        }
        
    }

    protected void CBF110029_DDL1_SelectedIndexChanged(object sender, EventArgs e)
    {
        CBF110029_cambridge.Text += $"<a href = 'https://dictionary.cambridge.org/zht/%E8%A9%9E%E5%85%B8/%E8%8B%B1%E8%AA%9E-%E6%BC%A2%E8%AA%9E-%E7%B9%81%E9%AB%94/{CBF110029_DDL1.SelectedItem}'>{CBF110029_DDL1.SelectedItem}</a> => {CBF110029_DDL1.SelectedValue} <br />";
    }

    protected void CBF110029_DDL1_DataBound(object sender, EventArgs e)
    {
        DDL_Page_change();
    }

    protected void CBF110029_PreviousButton_Click(object sender, EventArgs e)
    {
        CBF110029_NextButton.Enabled = true;
        Session["DDL_page"] = Convert.ToInt32(Session["DDL_page"]) - 1;
        CBF110029_DDL1.DataBind();
    }

    protected void CBF110029_NextButton_Click(object sender, EventArgs e)
    {
        CBF110029_PreviousButton.Enabled = true;
        Session["DDL_page"] = Convert.ToInt32(Session["DDL_page"]) + 1;
        CBF110029_DDL1.DataBind();
    }

    void DDL_Page_change()
    {
        if (Convert.ToInt32(Session["DDL_page"]) * 10 + 9 >= CBF110029_DDL1.Items.Count)
        {
            CBF110029_NextButton.Enabled = false;
        }
        if (Convert.ToInt32(Session["DDL_page"]) <= 0)
        {
            CBF110029_PreviousButton.Enabled = false;
        }
        //===========================================================================================
        for (int i = CBF110029_DDL1.Items.Count - 1; i > Convert.ToInt32(Session["DDL_page"])*10+9; i--)
        {
            CBF110029_DDL1.Items.Remove(CBF110029_DDL1.Items[i]);
        }
        if (Convert.ToInt32(Session["DDL_page"]) > 0)
        {
            for (int i = (Convert.ToInt32(Session["DDL_page"])*10) - 1; i >= 0; i--)
            {
                CBF110029_DDL1.Items.Remove(CBF110029_DDL1.Items[i]);
            }
        }
        //=====================================================================================
        //CBF110029_cambridge.Text = Session["DDL_page"].ToString();
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        CBF110029_DDL1.DataBind();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        CBF110029_DDL1.DataBind();
    }
    //============================================================================================

    
    protected void CBF110029_testBtn_Click(object sender, EventArgs e)
    {
        Random r = new Random();
        CBF110029_MV1.ActiveViewIndex = 1;
        List<int> c_item = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        //============================================================
        Session["QC"] = 0;
        //=====================================================================
        for (int i = CBF110029_DDL1.Items.Count - 1; i > 0; i--)
        {
            int x = r.Next(0,i);
            int t;

            t = c_item[x];
            c_item[x] = c_item[i];
            c_item[i] = t;
        }
        //=============================================================================
        for (int i = 0; i < 10; i++)
        {
            Session[$"Q{i}"] = c_item[i];
        }
        //================================================================================
        Session["score"] = 0;
        test_gos_on();
    }

    void test_gos_on()
    {
        CBF110029_ch_hint.Text += CBF110029_DDL1.Items[Convert.ToInt32(Session[$"Q{Convert.ToInt32(Session["QC"])}"])].Value.ToString(); //Session[$"Q{Convert.ToInt32(Session["QC"])}"].ToString();
        CBF110029_input.Text = CBF110029_DDL1.Items[Convert.ToInt32(Session[$"Q{Convert.ToInt32(Session["QC"])}"])].Text[0].ToString();
        for (int i = 1; i < CBF110029_DDL1.Items[Convert.ToInt32(Session[$"Q{Convert.ToInt32(Session["QC"])}"])].Text.Length; i++)
        {
            CBF110029_input.Text += " _";
        }
    }

    protected void CBF110029_nextQBtn_Click(object sender, EventArgs e)
    {
        if (CBF110029_nextQBtn.Text == "結束")
        {
            System.Environment.Exit(0);
        }
        else
        { 
            SetFocus(CBF110029_input);
            if (CBF110029_input.Text == CBF110029_DDL1.Items[Convert.ToInt32(Session[$"Q{Convert.ToInt32(Session["QC"])}"])].Text.ToString())
            {
                Session["score"] = Convert.ToInt32(Session["score"]) + 1;
                CBF110029_ch_hint.Text = "答對了! <br />";
            }
            else 
            {
                CBF110029_ch_hint.Text = $"答錯了! 正確答案是{CBF110029_DDL1.Items[Convert.ToInt32(Session[$"Q{Convert.ToInt32(Session["QC"])}"])].Text.ToString()} <br />";
            }
            Session["QC"] = Convert.ToInt32(Session["QC"]) + 1;
            if (Convert.ToInt32(Session["QC"]) < 8)
            {
                test_gos_on();
            }
            else 
            {
                CBF110029_nextQBtn.Text = "結束";
                HyperLink1.Visible = true;
                CBF110029_ch_hint.Text += $"您的得分 {Convert.ToInt32(Session["score"]) * 10}";
                CBF110029_input.Visible = false;
            }
        }
        

        
    }
}