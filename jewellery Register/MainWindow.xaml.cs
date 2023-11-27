using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace jewellery_Register
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Item> DataItem { get; set; }

       

        List<Item> items = new List<Item>();

        public MainWindow()
        {
            InitializeComponent();

            DataItem = new ObservableCollection<Item>();
            items_list.ItemsSource= DataItem;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (pure_gold_textbox != null && ratti_text_box != null && total_weight_text_box != null)
            {
                if (pure_gold_textbox.Text != "" && ratti_text_box.Text != "" && total_weight_text_box.Text != "")
                {
                    String pureGold = pure_gold_textbox.Text;
                    String ratti = ratti_text_box.Text;
                    String totalWeight = total_weight_text_box.Text;
                    String name = jewel_text_box.Text;
                    String goldRate = gold_rate_text_box.Text;
                    String total=total_text_box.Text;

                    DataItem.Add(new Item
                    {
                        id = DataItem.Count + 1,
                        name = name,
                        pure_gold = pureGold,
                        ratti = ratti,
                        total_weight = totalWeight,
                        gold_rate=  goldRate,
                        total=total,
                    });

                    

                }
            }

        }

        private void total_weight_text_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            calculatePureGold();
            if(IsInitialized == true)
            {
                if (total_weight_text_box.Text != "")
                {
                    labor_text_box.Text = (double.Parse(total_weight_text_box.Text) * 30).ToString();

                }
            }
        }

        private void ratti_text_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            calculatePureGold();
        }

        private void calculatePureGold()
        {
            if (pure_gold_textbox != null && ratti_text_box != null && total_weight_text_box != null)
            {
                if(pure_gold_textbox.Text!="" && ratti_text_box.Text!="" && total_weight_text_box.Text != "")
                {
                    try
                    {

                        double total_weight = double.Parse(total_weight_text_box.Text);
                        double ratti = double.Parse(ratti_text_box.Text);

                        double pure_gold = total_weight / 96 * (96 - ratti);
                        pure_gold_textbox.Text = Math.Round(pure_gold,2).ToString();

                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.ToString());
                    }
                }
            }
        }

        private void num1_TextChanged(object sender, TextChangedEventArgs e)
        {
            sumAllThree();
        }

        void sumAllThree()
        {
            if(num1!=null && num2!=null && num3!=null && numTotal!=null)
            {
                if(num1.Text!="" && num2.Text!=""&& num3.Text != "")
                {
                    double val1 = double.Parse(num1.Text);
                    double val2 = double.Parse(num2.Text);
                    double val3 = double.Parse(num3.Text);

                    double sum = val1 + val2 + val3;

                    numTotal.Text = Math.Round(sum, 2).ToString();
                }
            }
        }

        private void num2_TextChanged(object sender, TextChangedEventArgs e)
        {
            sumAllThree();

        }

        private void num3_TextChanged(object sender, TextChangedEventArgs e)
        {
            sumAllThree();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            FlowDocument doc = new FlowDocument(new Paragraph(new Run("Some text goes here")));

            doc.Name = "FlowDoc";

            IDocumentPaginatorSource idpSource = doc;

            printDlg.ShowDialog();

            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            documentView docWin = new documentView();
            // Create the application's main window
            docWin.Title = "Grid Sample";
            docWin.Width = 288;

            ScrollViewer a = new ScrollViewer();
            // Create the Grid
            Grid myGrid = new Grid();
            a.Content=myGrid;
            
            myGrid.Width = 288;
            myGrid.HorizontalAlignment = HorizontalAlignment.Left;
            myGrid.VerticalAlignment = VerticalAlignment.Top;
            

            // Define the Columns
            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            ColumnDefinition colDef3 = new ColumnDefinition();
            myGrid.ColumnDefinitions.Add(colDef1);
            myGrid.ColumnDefinitions.Add(colDef2);
            myGrid.ColumnDefinitions.Add(colDef3);

            // Define the Rows
            RowDefinition rowDef1 = new RowDefinition();
            myGrid.RowDefinitions.Add(rowDef1);

            // Heading: Row 0
            TextBlock txt1 = new TextBlock();
            txt1.Text = "MW Gold Laboratory";
            txt1.FontSize = 20;
            txt1.FontWeight = FontWeights.Bold;
            txt1.Height = 100;
            txt1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumnSpan(txt1, 3);
            Grid.SetRow(txt1, 0);

            //Date : Row 1
            TextBlock txt9 = new TextBlock();
            txt9.FontSize = 16;
            txt9.FontWeight = FontWeights.Bold;
            txt9.Text = "Date: " + DateTime.Now;
            Grid.SetRow(txt9, 1);
            Grid.SetColumnSpan(txt9, 3);
            myGrid.Children.Add(txt9);

            //Gold Rate : Row 2
            TextBlock txt10 = new TextBlock();
            txt10.FontSize = 16;
            txt10.FontWeight = FontWeights.Bold;
            txt10.Text = "Gold Rate: " + gold_rate_text_box.Text;
            Grid.SetRow(txt10, 2);
            Grid.SetColumnSpan(txt10, 3);
            myGrid.Children.Add(txt10);
            
            ////Gold Rate : Row 2
            //TextBlock txt11 = new TextBlock();
            //txt11.FontSize = 16;
            //txt11.FontWeight = FontWeights.Bold;
            //txt11.Text = "Labor: " + DateTime.Now;

            //Grid.SetRow(txt11, 2);
            //Grid.SetColumnSpan(txt11, 1);
            //myGrid.Children.Add(txt11);


            // Heading of Item: Row 3
            TextBlock txt2 = new TextBlock();
            txt2.Text = "Name";
            txt2.FontSize = 12;
            txt2.FontWeight = FontWeights.Bold;
            Grid.SetRow(txt2, 3);
            Grid.SetColumn(txt2, 0);

            // Add the third text cell to the Grid
            TextBlock txt3 = new TextBlock();
            txt3.Text = "Pure Gold";
            txt3.FontSize = 12;
            txt3.FontWeight = FontWeights.Bold;
            Grid.SetRow(txt3, 3);
            Grid.SetColumn(txt3, 1);

            // Add the fourth text cell to the Grid
            TextBlock txt4 = new TextBlock();
            txt4.Text = "Price";
            txt4.FontSize = 12;
            txt4.FontWeight = FontWeights.Bold;
            Grid.SetRow(txt4, 3);
            Grid.SetColumn(txt4, 2);


            RowDefinition rowDef3 = new RowDefinition();
            RowDefinition rowDef5 = new RowDefinition();
            RowDefinition rowDef4 = new RowDefinition();
            RowDefinition rowDef6 = new RowDefinition();
            myGrid.RowDefinitions.Add(rowDef3);
            myGrid.RowDefinitions.Add(rowDef4);
            myGrid.RowDefinitions.Add(rowDef5);
            myGrid.RowDefinitions.Add(rowDef6);

            //Headings Added
            myGrid.Children.Add(txt1);
            myGrid.Children.Add(txt2);
            myGrid.Children.Add(txt3);
            myGrid.Children.Add(txt4);

            int i = 0;
            double totalPrice=0;
            int row = 0;
            //foreach (Item item in DataItem)
            //{
            //    RowDefinition rowDef2 = new RowDefinition();
            //    myGrid.RowDefinitions.Add(rowDef2);
            //    row = i + 4;

            //    TextBlock txt5 = new TextBlock();

            //    txt5.Text = item.name;
            //    Grid.SetRow(txt5, row);
            //    Grid.SetColumn(txt5, 0);

            //    TextBlock txt6 = new TextBlock();

            //    txt6.Text = item.pure_gold;
            //    Grid.SetRow(txt6, row);
            //    Grid.SetColumn(txt6, 1);

            //    // Add the final text cell to the Grid
            //    TextBlock txt7 = new TextBlock();

            //    txt7.Text = item.total;
            //    Grid.SetRow(txt7, 2);
            //    Grid.SetColumn(txt7, row);
            //    totalPrice=(double.Parse(item.total)).ToString();

            //    myGrid.Children.Add(txt5);
            //    myGrid.Children.Add(txt6);
            //    myGrid.Children.Add(txt7);

            //}
            ObservableCollection<DisplayItem> dItems = new ObservableCollection<DisplayItem>();
           
            foreach(Item item in DataItem)
            {
                dItems.Add(new DisplayItem
                {
                    ID = item.id,
                    Name = item.name,

                    PureGold = item.pure_gold,
                    Total = item.total
                }) ;
                totalPrice += double.Parse(item.total);
            }

            DataGrid dataGrid = new DataGrid();
            dataGrid.ColumnWidth=50;
            
            dataGrid.ItemsSource = dItems;
            RowDefinition rowDef2 = new RowDefinition();
            myGrid.RowDefinitions.Add(rowDef2);
            Grid.SetRow(dataGrid, 4);
            Grid.SetColumnSpan(dataGrid, 3);
            myGrid.Children.Add(dataGrid);



            RowDefinition rowDef7 = new RowDefinition();
            myGrid.RowDefinitions.Add(rowDef7);


            TextBlock txt12 = new TextBlock();
            txt12.FontSize = 16;
            txt12.FontWeight = FontWeights.Bold;
            txt12.Text = "Total Price: " + totalPrice.ToString();
            Grid.SetRow(txt12, 5);
            Grid.SetColumnSpan(txt12, 3);
            myGrid.Children.Add(txt12);




            docWin.Content = a;
            docWin.Show();
            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintVisual(myGrid, "receipt printing ..");
        }

        private void gold_rate_text_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            calcTotal();
        }

        private void labor_text_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            calcTotal();
        }
       
        void calcTotal()
        {
            if (gold_rate_text_box != null && total_text_box != null && labor_text_box != null && numTotal != null && pure_gold_textbox!=null)
            {
                if (gold_rate_text_box.Text != "" && labor_text_box.Text != "" && pure_gold_textbox.Text!="")
                {
                    double rate = double.Parse(gold_rate_text_box.Text);
                    double labor = double.Parse(labor_text_box.Text);
                    double pure = double.Parse(pure_gold_textbox.Text);

                    double res = Math.Round((rate*pure) - labor,2);

                    total_text_box.Text = res.ToString();
                }
            }
        }

        private void pure_gold_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            calcTotal();
        }

        private void total_weight_text_box_KeyDown(object sender, KeyEventArgs e)
        {
            validateInput(e);
        }

        void validateInput(KeyEventArgs e)
        {
            if (!((e.Key >= Key.D0 && e.Key <= Key.D9) ||
                (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                e.Key == Key.Back ||
                e.Key == Key.Delete ||
                e.Key == Key.Decimal ||
                e.Key==Key.Tab))
            {
                // Suppress the key press if it's not a valid input
                e.Handled = true;
            }

            // Allow only one decimal point
            if (e.Key == Key.Decimal && total_weight_text_box.Text.Contains("."))
            {
                // Suppress the key press if there's already a decimal point
                e.Handled = true;
            }
        }

        private void ratti_text_box_KeyDown(object sender, KeyEventArgs e)
        {
            validateInput(e);

        }

        private void gold_rate_text_box_KeyDown(object sender, KeyEventArgs e)
        {
            validateInput(e);

        }

        private void total_weight_text_box_GotFocus(object sender, RoutedEventArgs e)
        {
            total_weight_text_box.Clear();
        }

        private void ratti_text_box_GotFocus(object sender, RoutedEventArgs e)
        {
            ratti_text_box.Clear();
        }

        private void gold_rate_text_box_GotFocus(object sender, RoutedEventArgs e)
        {
            gold_rate_text_box.Clear();
        }

        private void num1_GotFocus(object sender, RoutedEventArgs e)
        {
            num1.Clear();
        }

        private void num2_GotFocus(object sender, RoutedEventArgs e)
        {
            num2.Clear();
        }

        private void num3_GotFocus(object sender, RoutedEventArgs e)
        {
            num3.Clear();
        }
    }

    

}
