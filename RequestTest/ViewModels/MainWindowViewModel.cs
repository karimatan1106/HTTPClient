using AngleSharp.Parser.Html;
using Prism.Commands;
using Prism.Mvvm;
using System.Net.Http;

namespace RequestTest.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region コマンド
        public DelegateCommand HttpCommand { get; set; }
        #endregion

        #region フィールド
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly HtmlParser _htmlParser = new HtmlParser();
        #endregion

        #region コンストラクタ
        public MainWindowViewModel()
        {
            HttpCommand = new DelegateCommand(Fuga);
        }
        #endregion

        #region メソッド
        private async void Fuga()
        {
            var htmlString = await _httpClient.GetStringAsync("https://www.dailyfx.com/forex-rates");
            // AngleSharp.Parser.Html.HtmlParserオブジェクトにHTMLをパースさせる
            var parse = await _htmlParser.ParseAsync(htmlString);

            //#EURUSDは見つかる
            var eurUsd = parse.QuerySelector("#EURUSD")?.InnerHtml;

            //#eurusd-priceAskが見つからない
            var priceAsk = parse.QuerySelector("#eurusd-priceAsk")?.InnerHtml;
        }
        #endregion
    }
}
