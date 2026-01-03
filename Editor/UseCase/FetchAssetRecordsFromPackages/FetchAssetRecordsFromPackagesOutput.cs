using System.Collections.Generic;

namespace RiperBool.AssetPanel.Editor.UseCase.FetchAssetRecordsFromPackages
{
    public record FetchAssetRecordsFromPackagesOutput(
        List<AssetRecord> Records
    );
}