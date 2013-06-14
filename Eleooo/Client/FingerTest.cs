using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Eleooo.Client
{
    public partial class FingerTest : Form
    {
        public FingerTest( )
        {
            InitializeComponent( );
            //btnReadFinger.Enabled = false;
            //btnCloseDevice.Enabled = false;
        }


        private void btnOpenDevice_Click(object sender, EventArgs e)
        {
            //if (FingerHelpe.Instance.OpenDeviceEx() == 0)
            //{
            //    lblMessage.Text = "打开设备成功!";
            //}
            //else
            //{
            //    lblMessage.Text = "打开设备失败";
            //}
        }

        private void btnReadFinger_Click_old(object sender, EventArgs e)
        {
            //lblMessage.Text = "请把手指放到感应器上...";
            //byte[] buff = new byte[FingerHelpe.IMAGE_SIZE];
            //byte[] cBuff = new byte[FingerHelpe.CHAR_SIZE];
            //int imageLength = 0;
            //int cLength = 0;
            //int nRet = 0;
            //while ((nRet = FingerHelpe.Instance.GetImage( )) == 2)
            //{
            //    Application.DoEvents( );
            //}
            //if (nRet != 0)
            //{
            //    lblMessage.Text = FingerHelpe.Instance.LastMessage;
            //    return;
            //}
            //nRet = FingerHelpe.Instance.UpImage(ref buff[0], ref imageLength);
            //if (nRet == 0)
            //{
            //    nRet = FingerHelpe.Instance.PC_GenChar(ref buff[0],ref cBuff[0]);
            //    txtFingerText.Text = FingerHelpe.Instance.GetFingerText(cBuff);
            //    nRet = FingerHelpe.Instance.ShowFingerData(pbFingerPic.Handle,ref buff[0]);
            //    if (nRet == 0)
            //        lblMessage.Text = "读取成功";
            //    else
            //        lblMessage.Text = FingerHelpe.Instance.LastMessage;
            //}
            //else
            //{
            //    lblMessage.Text = "读取失败";
            //}
        }



        private void btnCloseDevice_Click(object sender, EventArgs e)
        {

        }

        private void FingerTest_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void btnReadFinger_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "准备读指纹";
            if (pbFingerPic.Image != null)
                pbFingerPic.Image.Dispose( );
            this.Update( );
            FingerPrint.Finger.BeginRead((fingerCode, fingerImg, message, isSuccess) =>
            {
                lblMessage.Text = message;
                if (isSuccess)
                {
                    txtFingerText.Text = fingerCode;
                    pbFingerPic.Image = Bitmap.FromFile(fingerImg);
                }
            });
        }

        private void btnMatch_Click(object sender, EventArgs e)
        {
            lblMessage.Text = FingerPrint.Finger.MatchFinger(txtFingerText.Text, txtFingerTest.Text).ToString( );
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FingerPrint.Finger.CancelRead( );
        }

        private void btnMatch2Fp_Click(object sender, EventArgs e)
        {
            lblMessage.Text = FingerPrint.Match2Fp(txtFingerText.Text, txtFingerTest.Text).ToString( );
        }

        private void btnSetTest_Click(object sender, EventArgs e)
        {
            txtFingerTest.Text = txtFingerText.Text;
        }

        private void btnGenChar_Click(object sender, EventArgs e)
        {
            //byte[] charData1 = FingerPrint.GenChar("000010.bmp");
            //byte[] charData2 = FingerPrint.GenChar("000011.bmp");
            //int nRet = FingerPrint.Match2Fp(charData1, charData2);
            //lblMessage.Text = nRet.ToString( );
        }
    }
}
