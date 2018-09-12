using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Communication.OMRON
{
  public   class OMRONVacuumConvert
    {

        ///<summary>
        /// 将ASCII格式十六进制字符串转浮点数（符合IEEE-754标准（32））
        /// </summary>
        /// <paramname="data">十六进制字符串</param>
        /// <returns>浮点数值</returns>
        public static float floatintStringToFloat(String data)
        {
            byte[] intBuffer = new byte[4];
            // 将16进制串按字节逆序化（一个字节2个ASCII码）
            for (int i = 0; i < 4; i++)
            {
                intBuffer[i] = Convert.ToByte(data.Substring((3 - i) * 2, 2), 16);
            }
            return BitConverter.ToSingle(intBuffer, 0);

        }
        /// <summary>
        /// 将二进制值转ASCII格式十六进制字符串
        /// </summary>
        /// <paramname="data">二进制值</param>
        /// <paramname="length">定长度的二进制</param>
        /// <returns>ASCII格式十六进制字符串</returns>
        public static string toHexString(int data, int length)
        {
            string result = "";
            if (data > 0)
                result = Convert.ToString(data, 16).ToUpper();
            if (result.Length < length)
            {
                // 位数不够补0
                StringBuilder msg = new StringBuilder(0);
                msg.Length = 0;
                msg.Append(result);
                for (; msg.Length < length; msg.Insert(0, "0")) ;
                result = msg.ToString();
            }
            return result;
        }
        ///<summary>
        /// 将浮点数转ASCII格式十六进制字符串（符合IEEE-754标准（32））
        /// </summary>
        /// <paramname="data">浮点数值</param>
        /// <returns>十六进制字符串</returns>
        public static string FloatToIntString(float data)
        {
            byte[] intBuffer = BitConverter.GetBytes(data);
            StringBuilder stringBuffer = new StringBuilder(0);
            for (int i = 0; i < intBuffer.Length; i++)
            {
                stringBuffer.Insert(0, toHexString(intBuffer[i] & 0xff, 2));
            }
            return stringBuffer.ToString();
        }

        /// <summary>
        /// 将ASCII格式十六进制字符串转浮点数（符合IEEE-754标准（32））
        /// </summary>
        /// <param name="data">16进制字符串</param>
        /// <returns></returns>
        public static float StringToFloat(String data)
        {
            if (data.Length < 8 || data.Length > 8)
            {
                //throw new NotEnoughDataInBufferException(data.length(), 8);
                return 0;
            }
            else
            {
                byte[] intBuffer = new byte[4];
                // 将16进制串按字节逆序化（一个字节2个ASCII码）
                for (int i = 0; i < 4; i++)
                {
                    intBuffer[i] = Convert.ToByte(data.Substring((3 - i) * 2, 2), 16);
                }
                return BitConverter.ToSingle(intBuffer, 0);
            }
        }
        /// <summary>
        /// 将byte数组转为浮点数
        /// </summary>
        /// <param name="bResponse">byte数组</param>
        /// <returns></returns>
        public static float ByteToFloat(byte[] bResponse)
        {
            if (bResponse.Length < 4 || bResponse.Length > 4)
            {
                //throw new NotEnoughDataInBufferException(data.length(), 8);
                return 0;
            }
            else
            {
                byte[] intBuffer = new byte[4];
                //将byte数组的前后两个字节的高低位换过来
                intBuffer[0] = bResponse[1];
                intBuffer[1] = bResponse[0];
                intBuffer[2] = bResponse[3];
                intBuffer[3] = bResponse[2];
                return BitConverter.ToSingle(intBuffer, 0);
            }
        }
        /// <summary>
        /// 用指针方式强制将byte数组转为浮点数
        /// </summary>
        /// <param name="bResponse"></param>
        /// <returns></returns>
        public static float BytetoFloatByPoint(byte[] bResponse)
        {
            //uint nRest = ((uint)response[startByte]) * 256 + ((uint)response[startByte + 1]) + 65536 * ((uint)response[startByte + 2]) * 256 + ((uint)response[startByte + 3]);
            float fValue = 0f;
            uint nRest = ((uint)bResponse[0]) * 256
                + ((uint)bResponse[1]) +
                65536 * (((uint)bResponse[2]) * 256 + ((uint)bResponse[3]));
            //用指针将整形强制转换成float
            unsafe
            {
                float* ptemp;
                ptemp = (float*)(&nRest);
                fValue = *ptemp;
            }
            return fValue;
        }
    }
}
