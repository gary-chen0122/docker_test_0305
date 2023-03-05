namespace NewebPay.Models
{
    public class HomeViewModel
    {
        public class SendToNewebPayIn
        {
			/// <summary>
			/// 特店編號(必填)
			/// </summary>
			public string MerchantID { get; set; }
			/// <summary>
			/// 特店訂單編號(必填)
			/// </summary>
			public string MerchantTradeNo { get; set; }
			/// <summary>
			/// 特店交易時間(必填)
			/// </summary>
			public string MerchantTradeDate { get; set; }
			/// <summary>
			/// 交易類型(必填)
			/// </summary>
			public string PaymentType { get; set; }
			/// <summary>
			/// 交易金額(必填)
			/// </summary>
			public string TotalAmount { get; set; }
			/// <summary>
			/// 交易描述(必填)
			/// </summary>
			public string TradeDesc { get; set; }
			/// <summary>
			/// 商品名稱(必填)
			/// </summary>
			public string ItemName { get; set; }
			/// <summary>
			/// 付款完成通知回傳網址(必填)
			/// </summary>
			public string ReturnURL { get; set; }
			/// <summary>
			/// 選擇預設付款方式(必填)
			/// </summary>
			public string ChoosePayment { get; set; }
			/// <summary>
			/// 檢查碼(必填)
			/// </summary>
			//public string CheckMacValue { get; set; }
			/// <summary>
			/// CheckMacValue加密類型(必填)
			/// </summary>
			public string EncryptType { get; set; }
            /// <summary>
            /// 特店旗下店舖代號
            /// </summary>
            public string StoreID { get; set; }
            /// <summary>
            /// Client端返回特店的按鈕連結
            /// </summary>
            public string ClientBackURL { get; set; }
            /// <summary>
            /// 商品銷售網址
            /// </summary>
            public string ItemURL { get; set; }
            /// <summary>
            /// 備註欄位
            /// </summary>
            public string Remark { get; set; }
            /// <summary>
            /// 付款子項目
            /// </summary>
            public string ChooseSubPayment { get; set; }
            /// <summary>
            /// Client端回傳付款結果網址
            /// </summary>
            public string OrderResultURL { get; set; }
            /// <summary>
            /// 是否需要額外的付款資訊
            /// </summary>
            public string NeedExtraPaidInfo { get; set; }
            /// <summary>
            /// 隱藏付款方式
            /// </summary>
            public string IgnorePayment { get; set; }
            /// <summary>
            /// 特約合作平台商代號
            /// </summary>
            public string PlatformID { get; set; }
            /// <summary>
            /// 自訂名稱欄位1
            /// </summary>
            public string CustomField1 { get; set; }
            /// <summary>
            /// 自訂名稱欄位2
            /// </summary>
            public string CustomField2 { get; set; }
            /// <summary>
            /// 自訂名稱欄位3
            /// </summary>
            public string CustomField3 { get; set; }
            /// <summary>
            /// 自訂名稱欄位4
            /// </summary>
            public string CustomField4 { get; set; }
            /// <summary>
            /// 語系設定
            /// </summary>
            public string Language { get; set; }
            /// <summary>
            /// 信用卡是否使用紅利折抵
            /// </summary>
            public string Redeem { get; set; }
            /// <summary>
            /// 銀聯卡交易選項
            /// </summary>
            public string UnionPay { get; set; }
            /// <summary>
            /// 記憶卡號
            /// </summary>
            public string BindingCard { get; set; }
            /// <summary>
            /// 記憶卡號識別碼
            /// </summary>
            public string MerchantMemberID { get; set; }
         
        }

        public class SendToNewebPayOut
        {
			/// <summary>
			/// 特店編號(必填)
			/// </summary>
			public string MerchantID { get; set; }
			/// <summary>
			/// 特店訂單編號(必填)
			/// </summary>
			public string MerchantTradeNo { get; set; }
			/// <summary>
			/// 特店交易時間(必填)
			/// </summary>
			public string MerchantTradeDate { get; set; }
			/// <summary>
			/// 交易類型(必填)
			/// </summary>
			public string PaymentType { get; set; }
			/// <summary>
			/// 交易金額(必填)
			/// </summary>
			public string TotalAmount { get; set; }
			/// <summary>
			/// 交易描述(必填)
			/// </summary>
			public string TradeDesc { get; set; }
			/// <summary>
			/// 商品名稱(必填)
			/// </summary>
			public string ItemName { get; set; }
			/// <summary>
			/// 付款完成通知回傳網址(必填)
			/// </summary>
			public string ReturnURL { get; set; }
			/// <summary>
			/// 選擇預設付款方式(必填)
			/// </summary>
			public string ChoosePayment { get; set; }
			/// <summary>
			/// 檢查碼(必填)
			/// </summary>
			public string CheckMacValue { get; set; }
			/// <summary>
			/// CheckMacValue加密類型(必填)
			/// </summary>
			public string EncryptType { get; set; }
            /// <summary>
            /// 特店旗下店舖代號
            /// </summary>
            public string StoreID { get; set; }
            /// <summary>
            /// Client端返回特店的按鈕連結
            /// </summary>
            public string ClientBackURL { get; set; }
            /// <summary>
            /// 商品銷售網址
            /// </summary>
            public string ItemURL { get; set; }
            /// <summary>
            /// 備註欄位
            /// </summary>
            public string Remark { get; set; }
            /// <summary>
            /// 付款子項目
            /// </summary>
            public string ChooseSubPayment { get; set; }
            /// <summary>
            /// Client端回傳付款結果網址
            /// </summary>
            public string OrderResultURL { get; set; }
            /// <summary>
            /// 是否需要額外的付款資訊
            /// </summary>
            public string NeedExtraPaidInfo { get; set; }
            /// <summary>
            /// 隱藏付款方式
            /// </summary>
            public string IgnorePayment { get; set; }
            /// <summary>
            /// 特約合作平台商代號
            /// </summary>
            public string PlatformID { get; set; }
            /// <summary>
            /// 自訂名稱欄位1
            /// </summary>
            public string CustomField1 { get; set; }
            /// <summary>
            /// 自訂名稱欄位2
            /// </summary>
            public string CustomField2 { get; set; }
            /// <summary>
            /// 自訂名稱欄位3
            /// </summary>
            public string CustomField3 { get; set; }
            /// <summary>
            /// 自訂名稱欄位4
            /// </summary>
            public string CustomField4 { get; set; }
            /// <summary>
            /// 語系設定
            /// </summary>
            public string Language { get; set; }
            /// <summary>
            /// 信用卡是否使用紅利折抵
            /// </summary>
            public string Redeem { get; set; }
            /// <summary>
            /// 銀聯卡交易選項
            /// </summary>
            public string UnionPay { get; set; }
            /// <summary>
            /// 記憶卡號
            /// </summary>
            public string BindingCard { get; set; }
            /// <summary>
            /// 記憶卡號識別碼
            /// </summary>
            public string MerchantMemberID { get; set; }
        }
    }
}
