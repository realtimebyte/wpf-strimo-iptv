﻿using StrimoLibrary.Models;
using StrimoLibrary.Services;
using StrimoUI.Components.Models;
using StrimoUI.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StrimoUI.Utilities
{
    public static class Utility
    {

        public static List<CarouselModel> ConvertStreamListToCarouselList<T>(List<T> streamList)
        {
            
            List<CarouselModel> carouselList = new List<CarouselModel>();

            string username = GlobalVars.currentAuthInfo.user_info.username;
            string password = GlobalVars.currentAuthInfo.user_info.password;


            Type streamType = typeof(T);
            foreach(T stream in streamList){
                CarouselModel temp = new CarouselModel();
                if(streamType.Equals(typeof(XCVodStreamModel))){
                    XCVodStreamModel vodStream = stream as XCVodStreamModel;
                    if (XtreamCodeService.GetImageWithStreamId(username, password, vodStream.stream_id, streamType) != null){
                        temp.CarouselItemTitle = vodStream.name;
                        temp.CarouselItemStreamId = vodStream.stream_id;
                        temp.CarouselItemImage = XtreamCodeService.GetImageWithStreamId(username, password, vodStream.stream_id, streamType);
                        temp.CarouselItemImageWidth = 670;
                        temp.CarouselItemImageHeight = 312;
                        temp.CarouselItemImageTop = 19;
                        temp.CarouselItemActive = false;
                        temp.StreamType = XCStreamType.VOD;
                        carouselList.Add(temp);
                    }
                } else if(streamType.Equals(typeof(XCSerieStreamModel))){
                    XCSerieStreamModel serieStream = stream as XCSerieStreamModel;
                    if (serieStream.backdrop_path[0] != null){
                        temp.CarouselItemTitle = serieStream.name;
                        temp.CarouselItemStreamId = serieStream.series_id;
                        temp.CarouselItemImage = serieStream.backdrop_path[0];
                        temp.CarouselItemImageWidth = 670;
                        temp.CarouselItemImageHeight = 312;
                        temp.CarouselItemImageTop = 19;
                        temp.CarouselItemActive = false;
                        temp.StreamType = XCStreamType.Serie;
                        carouselList.Add(temp);
                    }
                } else if(streamType.Equals(typeof(XCLiveStreamModel))){
                    XCLiveStreamModel liveStream = stream as XCLiveStreamModel;
                    if (liveStream.stream_icon != null){
                        temp.CarouselItemTitle = liveStream.name;
                        temp.CarouselItemStreamId = liveStream.stream_id;
                        temp.CarouselItemImage = liveStream.stream_icon;
                        temp.CarouselItemImageWidth = 274;
                        temp.CarouselItemImageHeight = 162;
                        temp.CarouselItemImageTop = 20;
                        temp.CarouselItemActive = false;
                        temp.StreamType = XCStreamType.Live;
                        carouselList.Add(temp);
                    }
                }
            }
            return carouselList;
        }

        public static string MakeLetterSpace(string OriginalString)
        {
            string ConvertedString = Regex.Replace(OriginalString, ".{1}", "$0 ");
            return ConvertedString;
        }
        public static string MilliSecondsToDate(string milliseconds)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(double.Parse(milliseconds)*1000).ToLocalTime();
            return dtDateTime.ToString();
        }
        public static string RemoveSpace(string OriginalString)
        {
            string trimmed = String.Concat(OriginalString.Where(c => !Char.IsWhiteSpace(c)));

            return trimmed;
        }

        public static string ExtractName(string name)
        {
            // Get last 6 chars from name....



            return null;
        }

        public static int ExtractDate(string name)
        {
            return 0;
        }
    }
}
