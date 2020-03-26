﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace MaFamille.Common.Converters
{
    public class BoolToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {            
            if (value == null)
                return false;
            else if(value is FileUploadStatus)
                return (FileUploadStatus)value == FileUploadStatus.Uploading ? false : true;
            else if(value is bool)
                return (bool)value;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {            
            //if (!(value is FileUploadStatus) || targetType != typeof(FileUploadStatus))
            //    return false;
            if (parameter == null)
            {
                if (value is FileUploadStatus)
                    return (FileUploadStatus)value == FileUploadStatus.Uploading ? false : true;
                else if (value is bool)
                    return (bool)value;
            }
            return false;
        }
    }

}
