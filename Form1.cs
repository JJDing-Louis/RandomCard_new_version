using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace RandomCard
{
    public partial class Form1 : Form
    {
        private int cardNumber = 52;//宣告牌數

        /// <summary>
        /// 宣告手牌空間
        /// </summary>
        private List<int> card = new List<int>();

        public Form1()
        {
            InitializeComponent();
            //以下這一段是dataGridView
            /* dataGridView1.ColumnCount = 2;
             dataGridView1.Columns[0].Name = "玩家";
             dataGridView1.Columns[1].Name = "手牌";*/
            getCard();
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            //DataGridView內容重置(判斷有無內容)

            if (dataGridView1.Columns.Count != 0)
            {
                dataGridView1.Columns.Clear();
            }

            if (int.TryParse(txtPlayer.Text, out int numPlayer)
                && numPlayer > 0)
            {
                if (numPlayer <= cardNumber)
                {
                    Reshuffle();
                    //dataGridView1.ColumnCount = numPlayer; //欄數為玩家人數
                    int getCardNumber = card.Count / numPlayer; //每個人分到的牌
                    //int[,] cardIndex = new int[getCardNumber, numPlayer];//建立陣列
                    for (int i = 0; i < numPlayer; i++)
                    {
                        dataGridView1.Columns.Add(string.Empty, $"玩家{(i + 1)}"); //命名玩家
                    }

                    //將牌發到陣列中
                    for (int i = 0; i < getCardNumber; i++)
                    {
                        string[] row = new string[numPlayer];

                        for (int j = 0; j < numPlayer; j++)
                        {
                            row[j] = card[(j * getCardNumber) + i].ToString();
                        }

                        dataGridView1.Rows.Add(row);
                        //for (int j = 0; j < getCardNumber; j++)
                        //{
                        //    dataGridView1.Rows.ad
                        //    cardIndex[j, i] = card[(i * getCardNumber) + j];
                        //}
                    }

                    //以下轉成列插入
                    //for (int i = 0; i < getCardNumber; i++)
                    //{
                    //    string[] row = new string[numPlayer];
                    //    for (int j = 0; j < numPlayer; j++)
                    //    {
                    //        row[j] = $"{cardIndex[i, j]}";
                    //    }
                    //    dataGridView1.Rows.Add(row);
                    //}

                    //此區寫剩下的牌數
                    richTextBox1.Clear();
                    int cardLeft = card.Count % numPlayer;
                    if (cardLeft != 0)
                    {
                        //20210327
                        //string txtMessage = string.Empty; //顯示訊息

                        StringBuilder build = new StringBuilder();

                        build.Append($"剩下牌數{cardLeft}\n");
                        for (int h = numPlayer * getCardNumber; h < cardNumber; h++)
                        {
                            build.Append($"{card[h]},");
                        }
                        richTextBox1.AppendText(build.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("玩家太多，且牌不夠!!!");
                };
            }
            else
            {
                MessageBox.Show("請輸入正確數字");
            }
            //清除輸入訊息框
            txtPlayer.Clear();
        }

        /// <summary>
        /// 產生52張牌
        /// </summary>
        /// <returns></returns>
        public void getCard()
        {
            //List<int> card = new List<int>();
            for (int i = 1; i <= cardNumber; i++)
            {
                card.Add(i);
            }
        }

        /// <summary>
        /// 以下為洗牌動作
        /// </summary>
        public void Reshuffle()
        {
            Random rnd = new Random();//亂數
            //List<int> temp = new List<int>();//建立暫存空間
            //for (int i = cardNumber; i > 0; i--)
            //{
            //    int c = rnd.Next(0, i);
            //    temp.Add(card[c]);
            //    card.Remove(card[c]);
            //}
            for (int i = 0; i < cardNumber; i++)
            {
                int pick_Index = rnd.Next(0, cardNumber);
                int temp = card[i];
                card[i] = card[pick_Index];
                card[pick_Index] = temp;
            }
        }
    }
}