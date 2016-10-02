using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using System.Linq;

namespace QuoteGenerator
{
    [Activity(Label = "Quote Generator", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button generateQuote = FindViewById<Button>(Resource.Id.generate);
            TextView outputQuote = FindViewById<TextView>(Resource.Id.quoteOutput);
            // categories
            RadioButton wisdom = FindViewById<RadioButton>(Resource.Id.wisdom);
            RadioButton funny = FindViewById<RadioButton>(Resource.Id.funny);
            RadioButton intelligent = FindViewById<RadioButton>(Resource.Id.intelligent);

            AssetManager assets = this.Assets;
            List<string> wisdomQuotes = getQuotes("quotes_wisdom.txt", assets);
            List<string> funnyQuotes = getQuotes("quotes_funny.txt", assets);
            List<string> intelligentQuotes = getQuotes("quotes_intelligent.txt", assets);

            wisdom.Click += RadioButtonClick;
            funny.Click += RadioButtonClick;
            intelligent.Click += RadioButtonClick;

            Random r = new Random();

            generateQuote.Click += (object sender, EventArgs e) =>
            {
                if (funny.Checked == true)
                    outputQuote.Text = funnyQuotes[r.Next(0, funnyQuotes.Count - 1)];
                if (intelligent.Checked == true)
                    outputQuote.Text = intelligentQuotes[r.Next(0, intelligentQuotes.Count - 1)];
                if (wisdom.Checked == true)
                    outputQuote.Text = wisdomQuotes[r.Next(0, wisdomQuotes.Count - 1)];
            };
    }

    protected List<string> getQuotes(string quoteFile, AssetManager assets)
    {
        string content;
        using (StreamReader sr = new StreamReader(assets.Open(quoteFile)))
        {
            content = sr.ReadToEnd();
        }
        // splits the quote-file into individual quotes and stores it in a list
        List<String> sentences = Regex.Split(content, "</s>").ToList();
        return sentences;
    }

    private void RadioButtonClick(object sender, EventArgs e)
    {
        RadioButton rb = (RadioButton)sender;
        Toast.MakeText(this, rb.Text, ToastLength.Short).Show();
    }
}
}

