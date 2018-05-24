using Microsoft.Toolkit.Uwp.Notifications;
using piggy_bank_uwp.ViewModels.Diagram;
using System;
using System.Collections.Generic;

namespace piggy_bank_uwp.Services
{
    public static class TileService
    {
        public static TileContent GenerateTileContent(List<DataDiagramViewModel> datas)
        {
            return new TileContent()
            {
                Visual = new TileVisual()
                {
                    DisplayName = "PiggyBank",
                    TileMedium = GenerateTileBinding(datas),
                    TileWide = GenerateTileBinding(datas),
                    TileLarge = GenerateTileBinding(datas)
                }
            };
        }

        private static TileBinding GenerateTileBinding(List<DataDiagramViewModel> datas)
        {
            TileBinding binding = new TileBinding { DisplayName = "PiggyBank" };
            TileBindingContentAdaptive content = new TileBindingContentAdaptive();

            foreach (var data in datas)
            {
                content.Children.Add(new AdaptiveText
                {
                    Text = String.Format("{0} {1:##}%", data.Title, data.Value),
                    HintStyle = AdaptiveTextStyle.CaptionSubtle
                });
            }

            binding.Content = content;

            return binding;
        }
    }
}
