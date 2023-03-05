using Microsoft.AspNetCore.Mvc;
using NewebPay.Models;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;
using System.Security.Cryptography;
using static NewebPay.Models.HomeViewModel;
using System.Reflection.Metadata;
using System.Security.Cryptography.Xml;


namespace NewebPay.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
			// 產生測試資訊
			ViewData["MerchantID"] = Config.GetSection("MerchantID").Value;                  //特店編號
			ViewData["MerchantTradeNo"] = "Aa02260001";                                      //特店訂單編號
			ViewData["MerchantTradeDate"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");    //特店交易時間
			ViewData["ReturnURL"] = $"{Request.Scheme}://{Request.Host}{Request.Path}Home/CallbackNotify";         //付款完成通知回傳網址
			ViewData["ClientBackURL"] = $"{Request.Scheme}://{Request.Host}{Request.Path}";                        //Client端返回特店的按鈕連結
			ViewData["OrderResultURL"] = $"{Request.Scheme}://{Request.Host}{Request.Path}Home/CallbackReturn";    //Client端回傳付款結果網址
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		/// <summary>
		/// 傳送訂單至綠界金流
		/// </summary>
		/// <param name="inModel"></param>
		/// <returns></returns>
		[ValidateAntiForgeryToken]
		public IActionResult SendToNewebPay(SendToNewebPayIn inModel)
		{
			SendToNewebPayOut outModel = new SendToNewebPayOut();

			// 綠界金流線上付款

			//交易欄位
			List<KeyValuePair<string, string>> TradeInfo = new List<KeyValuePair<string, string>>();

			// 特店編號(必填)
			TradeInfo.Add(new KeyValuePair<string, string>("MerchantID", inModel.MerchantID));
			// 特店訂單編號(必填)
			TradeInfo.Add(new KeyValuePair<string, string>("MerchantTradeNo", inModel.MerchantTradeNo));
			// 特店交易時間(必填)
			TradeInfo.Add(new KeyValuePair<string, string>("MerchantTradeDate", inModel.MerchantTradeDate));
			// 交易類型(必填)
			TradeInfo.Add(new KeyValuePair<string, string>("PaymentType", inModel.PaymentType));
			// 交易金額(必填)
			TradeInfo.Add(new KeyValuePair<string, string>("TotalAmount", inModel.TotalAmount));
			// 交易描述(必填)
			TradeInfo.Add(new KeyValuePair<string, string>("TradeDesc", inModel.TradeDesc));
			// 商品名稱(必填)
			TradeInfo.Add(new KeyValuePair<string, string>("ItemName", inModel.ItemName));
			// 付款完成通知回傳網址(必填)
			TradeInfo.Add(new KeyValuePair<string, string>("ReturnURL", inModel.ReturnURL));
			// 選擇預設付款方式(必填)
			TradeInfo.Add(new KeyValuePair<string, string>("ChoosePayment", inModel.ChoosePayment));
			// CheckMacValue加密類型(必填)
			TradeInfo.Add(new KeyValuePair<string, string>("EncryptType", inModel.EncryptType));
			// 特店旗下店舖代號
			TradeInfo.Add(new KeyValuePair<string, string>("StoreID", string.Empty));
			// Client端返回特店的按鈕連結
			TradeInfo.Add(new KeyValuePair<string, string>("ClientBackURL", inModel.ClientBackURL));
			// 商品銷售網址
			TradeInfo.Add(new KeyValuePair<string, string>("ItemURL", string.Empty));
			// 備註欄位
			TradeInfo.Add(new KeyValuePair<string, string>("Remark", string.Empty));
			// 付款子項目
			TradeInfo.Add(new KeyValuePair<string, string>("ChooseSubPayment", string.Empty));
			// Client端回傳付款結果網址
			TradeInfo.Add(new KeyValuePair<string, string>("OrderResultURL", inModel.OrderResultURL));
			// 是否需要額外的付款資訊
			TradeInfo.Add(new KeyValuePair<string, string>("NeedExtraPaidInfo", string.Empty));
			// 隱藏付款方式
			TradeInfo.Add(new KeyValuePair<string, string>("IgnorePayment", string.Empty));
			// 特約合作平台商代號
			TradeInfo.Add(new KeyValuePair<string, string>("PlatformID", string.Empty));
			// 自訂名稱欄位1
			TradeInfo.Add(new KeyValuePair<string, string>("CustomField1", string.Empty));
			// 自訂名稱欄位2
			TradeInfo.Add(new KeyValuePair<string, string>("CustomField2", string.Empty));
			// 自訂名稱欄位3
			TradeInfo.Add(new KeyValuePair<string, string>("CustomField3", string.Empty));
			// 自訂名稱欄位4
			TradeInfo.Add(new KeyValuePair<string, string>("CustomField4", string.Empty));
			// 語系設定
			TradeInfo.Add(new KeyValuePair<string, string>("Language", string.Empty));
			// 信用卡是否使用紅利折抵
			TradeInfo.Add(new KeyValuePair<string, string>("Redeem", string.Empty));
			// 銀聯卡交易選項
			TradeInfo.Add(new KeyValuePair<string, string>("UnionPay", string.Empty));
			// 記憶卡號
			TradeInfo.Add(new KeyValuePair<string, string>("BindingCard", string.Empty));
			// 記憶卡號識別碼
			TradeInfo.Add(new KeyValuePair<string, string>("MerchantMemberID", string.Empty));


			//檢查碼使用的
			List<KeyValuePair<string, string>> CheckValueInfo = new List<KeyValuePair<string, string>>();
			//檢查碼計算排序
			CheckValueInfo = TradeInfo.OrderBy(x => x.Key).ToList();


			// API 傳送欄位
			// 特店編號(必填)
			outModel.MerchantID = inModel.MerchantID;
			// 特店訂單編號(必填)
			outModel.MerchantTradeNo = inModel.MerchantTradeNo;
			// 特店交易時間(必填)
			outModel.MerchantTradeDate = inModel.MerchantTradeDate;
			// 交易類型(必填)
			outModel.PaymentType = inModel.PaymentType;
			// 交易金額(必填)
			outModel.TotalAmount = inModel.TotalAmount;
			// 交易描述(必填)
			outModel.TradeDesc = inModel.TradeDesc;
			// 商品名稱(必填)
			outModel.ItemName = inModel.ItemName;
			// 交易金額(必填)
			outModel.TotalAmount = inModel.TotalAmount;
			// 付款完成通知回傳網址(必填)
			outModel.ReturnURL = inModel.ReturnURL;
			// 選擇預設付款方式(必填)
			outModel.ChoosePayment = inModel.ChoosePayment;
			// CheckMacValue加密類型(必填)
			outModel.EncryptType = inModel.EncryptType;
			// 特店旗下店舖代號
			outModel.StoreID = string.Empty;
			// Client端返回特店的按鈕連結
			outModel.ClientBackURL = inModel.ClientBackURL;
			// 商品銷售網址
			outModel.ItemURL = string.Empty;
			// 備註欄位
			outModel.Remark = string.Empty;
			// 付款子項目
			outModel.ChooseSubPayment = string.Empty;
			// Client端回傳付款結果網址
			outModel.OrderResultURL = inModel.OrderResultURL;
			// 是否需要額外的付款資訊
			outModel.NeedExtraPaidInfo = string.Empty;
			// 隱藏付款方式
			outModel.IgnorePayment = string.Empty;
			// 特約合作平台商代號
			outModel.PlatformID = string.Empty;
			// 自訂名稱欄位1
			outModel.CustomField1 = string.Empty;
			// 自訂名稱欄位2
			outModel.CustomField2 = string.Empty;
			// 自訂名稱欄位3
			outModel.CustomField3 = string.Empty;
			// 自訂名稱欄位4
			outModel.CustomField4 = string.Empty;
			// 語系設定
			outModel.Language = string.Empty;
			// 信用卡是否使用紅利折抵
			outModel.Redeem = string.Empty;
			// 銀聯卡交易選項
			outModel.UnionPay = string.Empty;
			// 記憶卡號
			outModel.BindingCard = string.Empty;
			// 記憶卡號識別碼
			outModel.MerchantMemberID = string.Empty;


			// 檢查碼計算排序 + "&"
			string TradeInfoParam = string.Join("&", CheckValueInfo.Select(x => $"{x.Key}={x.Value}"));
			// 交易資料 AES 加解密
			IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
			// API 串接金鑰
			string HashKey = Config.GetSection("HashKey").Value;
			// API 串接密碼
			string HashIV = Config.GetSection("HashIV").Value;

			//交易資料 SHA256 加密
			outModel.CheckMacValue = EncryptSHA256($"HashKey={HashKey}&{TradeInfoParam}&HashIV={HashIV}");

			return Json(outModel);
		}


		/// <summary>
		/// 支付完成返回網址
		/// </summary>
		/// <returns></returns>
		public IActionResult CallbackReturn()
		{
			// 接收參數
			StringBuilder receive = new StringBuilder();
			foreach (var item in Request.Form)
			{
				receive.AppendLine(item.Key + "=" + item.Value + "<br>");
			}
			ViewData["ReceiveObj"] = receive.ToString();

			// 解密訊息
			IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
			string HashKey = Config.GetSection("HashKey").Value;//API 串接金鑰
			string HashIV = Config.GetSection("HashIV").Value;//API 串接密碼

			string TradeInfoDecrypt = DecryptAESHex(Request.Form["TradeInfo"], HashKey, HashIV);
			NameValueCollection decryptTradeCollection = HttpUtility.ParseQueryString(TradeInfoDecrypt);
			receive.Length = 0;
			foreach (String key in decryptTradeCollection.AllKeys)
			{
				receive.AppendLine(key + "=" + decryptTradeCollection[key] + "<br>");
			}
			ViewData["TradeInfo"] = receive.ToString();

			return View();
		}

		/// <summary>
		/// 商店取號網址
		/// </summary>
		/// <returns></returns>
		public IActionResult CallbackCustomer()
		{
			// 接收參數
			StringBuilder receive = new StringBuilder();
			foreach (var item in Request.Form)
			{
				receive.AppendLine(item.Key + "=" + item.Value + "<br>");
			}
			ViewData["ReceiveObj"] = receive.ToString();

			// 解密訊息
			IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
			string HashKey = Config.GetSection("HashKey").Value;//API 串接金鑰
			string HashIV = Config.GetSection("HashIV").Value;//API 串接密碼
			string TradeInfoDecrypt = DecryptAESHex(Request.Form["TradeInfo"], HashKey, HashIV);
			NameValueCollection decryptTradeCollection = HttpUtility.ParseQueryString(TradeInfoDecrypt);
			receive.Length = 0;
			foreach (String key in decryptTradeCollection.AllKeys)
			{
				receive.AppendLine(key + "=" + decryptTradeCollection[key] + "<br>");
			}
			ViewData["TradeInfo"] = receive.ToString();
			return View();
		}

		/// <summary>
		/// 支付通知網址
		/// </summary>
		/// <returns></returns>
		public IActionResult CallbackNotify()
		{
			// 接收參數
			StringBuilder receive = new StringBuilder();
			foreach (var item in Request.Form)
			{
				receive.AppendLine(item.Key + "=" + item.Value + "<br>");
			}
			ViewData["ReceiveObj"] = receive.ToString();

			// 解密訊息
			IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
			string HashKey = Config.GetSection("HashKey").Value;//API 串接金鑰
			string HashIV = Config.GetSection("HashIV").Value;//API 串接密碼
			string TradeInfoDecrypt = DecryptAESHex(Request.Form["TradeInfo"], HashKey, HashIV);
			NameValueCollection decryptTradeCollection = HttpUtility.ParseQueryString(TradeInfoDecrypt);
			receive.Length = 0;
			foreach (String key in decryptTradeCollection.AllKeys)
			{
				receive.AppendLine(key + "=" + decryptTradeCollection[key] + "<br>");
			}
			ViewData["TradeInfo"] = receive.ToString();

			return View();
		}

		/// <summary>
		/// 字串加密SHA256
		/// </summary>
		/// <param name="source">加密前字串</param>
		/// <returns>加密後字串</returns>
		public string EncryptSHA256(string source)
		{
			string szCheckMacValue = String.Empty;
			// 產生檢查碼。
			
			szCheckMacValue = HttpUtility.UrlEncode(source).ToLower();

			szCheckMacValue = SHA256Encoder.Encrypt(szCheckMacValue);

			return szCheckMacValue;
		}

		/// <summary>
		/// 16 進制字串解密
		/// </summary>
		/// <param name="source">加密前字串</param>
		/// <param name="cryptoKey">加密金鑰</param>
		/// <param name="cryptoIV">cryptoIV</param>
		/// <returns>解密後的字串</returns>
		public string DecryptAESHex(string source, string cryptoKey, string cryptoIV)
		{
			string result = string.Empty;

			if (!string.IsNullOrEmpty(source))
			{
				// 將 16 進制字串 轉為 byte[] 後
				byte[] sourceBytes = ToByteArray(source);

				if (sourceBytes != null)
				{
					// 使用金鑰解密後，轉回 加密前 value
					result = Encoding.UTF8.GetString(DecryptAES(sourceBytes, cryptoKey, cryptoIV)).Trim();
				}
			}

			return result;
		}

		/// <summary>
		/// 將16進位字串轉換為byteArray
		/// </summary>
		/// <param name="source">欲轉換之字串</param>
		/// <returns></returns>
		public byte[] ToByteArray(string source)
		{
			byte[] result = null;

			if (!string.IsNullOrWhiteSpace(source))
			{
				var outputLength = source.Length / 2;
				var output = new byte[outputLength];

				for (var i = 0; i < outputLength; i++)
				{
					output[i] = Convert.ToByte(source.Substring(i * 2, 2), 16);
				}
				result = output;
			}

			return result;
		}

		/// <summary>
		/// 字串解密AES
		/// </summary>
		/// <param name="source">解密前字串</param>
		/// <param name="cryptoKey">解密金鑰</param>
		/// <param name="cryptoIV">cryptoIV</param>
		/// <returns>解密後字串</returns>
		public static byte[] DecryptAES(byte[] source, string cryptoKey, string cryptoIV)
		{
			byte[] dataKey = Encoding.UTF8.GetBytes(cryptoKey);
			byte[] dataIV = Encoding.UTF8.GetBytes(cryptoIV);

			using (var aes = System.Security.Cryptography.Aes.Create())
			{
				aes.Mode = System.Security.Cryptography.CipherMode.CBC;
				// 智付通無法直接用PaddingMode.PKCS7，會跳"填補無效，而且無法移除。"
				// 所以改為PaddingMode.None並搭配RemovePKCS7Padding
				aes.Padding = System.Security.Cryptography.PaddingMode.None;
				aes.Key = dataKey;
				aes.IV = dataIV;

				using (var decryptor = aes.CreateDecryptor())
				{
					byte[] data = decryptor.TransformFinalBlock(source, 0, source.Length);
					int iLength = data[data.Length - 1];
					var output = new byte[data.Length - iLength];
					Buffer.BlockCopy(data, 0, output, 0, output.Length);
					return output;
				}
			}
		}
	}



	internal static class SHA256Encoder
	{
		/// <summary>
		/// 雜湊加密演算法物件。
		/// </summary>
		private static readonly HashAlgorithm Crypto = null;

		static SHA256Encoder()
		{
			SHA256Encoder.Crypto = new SHA256CryptoServiceProvider();
		}

		public static string Encrypt(string originalString)
		{
			byte[] source = Encoding.Default.GetBytes(originalString);//將字串轉為Byte[]
			byte[] crypto = SHA256Encoder.Crypto.ComputeHash(source);//進行SHA256加密
			string result = string.Empty;

			for (int i = 0; i < crypto.Length; i++)
			{
				result += crypto[i].ToString("X2");
			}

			return result.ToUpper();
		}
	}
}