using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System.Data;
using UnityEngine.Analytics;

public class Login : MonoBehaviour
{
    // Login UI
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_InputField usernameReg;
    public TMP_InputField passwordReg;
    public RawImage remind;
    public RawImage registerPage;
    public TMP_Text tip;
    private string sqlSer;
    private MySqlConnection conn;
    private int i = 2;

    void Start()
    {
        // 使名为"remind"的rawImage组件不可见
        remind.gameObject.SetActive(false);
        registerPage.gameObject.SetActive(false);
        sqlSer = "";
        conn = new MySqlConnection(sqlSer);
    }

    // Update is called once per frame
    void Update()
    {
        print(username.text);
        print(password.text);
    }
    public void LoginButton()
    {
        //// 判断用户名和密码是否正确
        //if (username.text == "admin" && password.text == "admin")
        //{
        //    print("Login Success");
        //    // 跳转到游戏场景
        //    UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        //}
        //else if (username.text != "admin" || password.text != "admin")
        //{
        //    print("用户名或密码错误");
        //    // 让remindText的文本内容为"用户名或密码错误"
        //    tip.text = "用户名或密码错误";
        //    // 使名为"remind"的rawImage组件出现
        //    remind.gameObject.SetActive(true);
        //}

        //Msql
        
        try
        { 
            conn.Open();
            Debug.Log("connect successful");
            //sql语句
            string sqlQuary = "SELECT * FROM player;";

            Debug.Log(sqlQuary);

            MySqlCommand comd = new MySqlCommand(sqlQuary, conn);

            MySqlDataReader reader = comd.ExecuteReader();

            while (reader.Read())
            {
                //通过reader获得数据库信息
                Debug.Log(reader.GetString("name"));
                Debug.Log(reader.GetString("password"));
                if (reader.GetString("name") == username.text && reader.GetString("password") == password.text)
                {
                    Debug.Log("登录成功");
                    // 跳转到游戏场景
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
                }
                else
                {
                    Debug.Log("失败");
                    // 让remindText的文本内容为"用户名或密码错误"
                    tip.text = "用户名或密码错误";
                    i = 2;
                    // 使名为"remind"的rawImage组件出现
                    remind.gameObject.SetActive(true);
                }
            }
            reader.Close();
        }
        catch (System.Exception e)
        {
            Debug.Log("Error:" + e.Message);
        }
        finally
        {
            conn.Close();
        }
    }

    public void BackButton()
    {
        // 使名为"remind"的rawImage组件不可见
        if (i == 2)
        {
            remind.gameObject.SetActive(false);
            registerPage.gameObject.SetActive(false);
        } else if(i == 1)
        {
            remind.gameObject.SetActive(false);
        }
        i = 2;
    }

    public void RegisterButton()
    {
        // 使注册界面出现
        registerPage.gameObject.SetActive(true);
    }

    public void RegisterPage_registerButton()
    {
        // 注册功能实现
        try
        {
            conn.Open();
            Debug.Log("connect successful");
            //sql语句
            string sqlQuary = "select * from player";

            Debug.Log(sqlQuary);

            MySqlCommand comd = new MySqlCommand(sqlQuary, conn);// 创建MySqlCommand对象，负责对数据库进行操作
            
            MySqlDataReader reader = comd.ExecuteReader();// 读取数据库

            if(usernameReg.text == "" || passwordReg.text == "")
            {
                Debug.Log("失败");

                tip.text = "用户名或密码不能为空！";
                i = 1;
                // 使名为"remind"的rawImage组件出现
                remind.gameObject.SetActive(true);
            }
            else
            {
                int flag = 0;
                while (reader.Read())
                {
                    Debug.Log(reader.GetString("name"));
                    Debug.Log(reader.GetString("password"));
                    if (reader.GetString("name") == usernameReg.text)
                    {
                        Debug.Log("失败");

                        tip.text = "该用户已存在！";

                        i = 1;
                        flag = 1;
                        // 使名为"remind"的rawImage组件出现
                        remind.gameObject.SetActive(true);
                        break;
                    }
                }
                reader.Close();
                if(flag == 0)
                {
                    Debug.Log("成功");

                    string sqlQuary2 = "insert into player(name,password,play_time,played_round,max_score,Number_of_cooperation) values ('" + usernameReg.text + "','" + passwordReg.text + "',0,0,0,0)";

                    Debug.Log(sqlQuary2);

                    MySqlCommand comd2 = new MySqlCommand(sqlQuary2, conn);// 创建MySqlCommand对象，负责对数据库进行操作

                    int rowsAffected = comd2.ExecuteNonQuery(); // 执行SQL查询  

                    Debug.Log("插入的行数：" + rowsAffected);

                    // 注册成功提示
                    tip.text = "注册成功";
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Error:" + e.Message);
        }
        finally
        {
            conn.Close();
        }
        remind.gameObject.SetActive(true);
    }
}
