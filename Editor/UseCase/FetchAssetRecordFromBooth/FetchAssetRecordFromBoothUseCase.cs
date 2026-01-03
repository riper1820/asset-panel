using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RiperBool.AssetPanel.Editor.UseCase.FetchAssetRecordFromBooth
{
    public class FetchAssetRecordFromBoothUseCase : IAsyncUseCase<FetchAssetRecordFromBoothInput, FetchAssetRecordFromBoothOutput>
    {
        private static readonly HttpClient HttpClient = new();
        // Example title: "<title>Awesome 3D Model - John Doe - BOOTH</title>"
        private static readonly Regex TitlePatternRegex = new("<title>(.*?) - (.*?) - BOOTH</title>", RegexOptions.Compiled);
        
        public async Task<FetchAssetRecordFromBoothOutput> Execute(FetchAssetRecordFromBoothInput input)
        {
            string html = await HttpClient.GetStringAsync(input.BoothUrl.Value);
            
            var assetRecord = ParseBoothPage(html, input.BoothUrl.Value);

            return new FetchAssetRecordFromBoothOutput(assetRecord);
        }
        
        private static AssetRecord ParseBoothPage(string html, Uri boothUrl)
        {
            var match = TitlePatternRegex.Match(html);
            if (!match.Success)
            {
                throw new Exception("Failed to parse booth page title.");
            }
            var assetName = match.Groups[1].Value.Trim();
            var author = match.Groups[2].Value.Trim();
            
            return new AssetRecord(assetName, author, boothUrl.OriginalString);
        }
    }
}