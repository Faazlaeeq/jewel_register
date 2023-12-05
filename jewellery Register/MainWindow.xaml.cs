using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;

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
            items_list.ItemsSource = DataItem;
            //write code to custumize each column in datagrid
         


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
                    String total = total_text_box.Text;
                    String labor = labor_text_box.Text;
                    String goldStatus = img_TakeGold.Visibility==Visibility.Visible?"L":"D";  
                    String cashStatus = img_Givemoney.Visibility == Visibility.Visible ? "L" : "D";

                    DataItem.Add(new Item
                    {
                        id = DataItem.Count + 1,
                        name = name,
                        pure_gold = pureGold,
                        ratti = ratti,
                        total_weight = totalWeight,
                        gold_rate = goldRate,
                        total = total,
                        labor = labor,
                        cashStatus = cashStatus,
                        goldStatus = goldStatus
                    });



                }
            }

        }

        private void total_weight_text_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            calculatePureGold();
            if (IsInitialized == true)
            {
                if (total_weight_text_box.Text != "")
                {
                    double gold = double.Parse(total_weight_text_box.Text);

                    labor_text_box.Text = gold>11? (gold * 30).ToString():350.ToString();

                }
            }
        }

        private void ratti_text_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            calculatePureGold();
            calcTotal();

        }

        private void calculatePureGold()
        {
            if (pure_gold_textbox != null && ratti_text_box != null && total_weight_text_box != null)
            {
                if (pure_gold_textbox.Text != "" && ratti_text_box.Text != "" && total_weight_text_box.Text != "")
                {
                    try
                    {

                        double total_weight = double.Parse(total_weight_text_box.Text);
                        double ratti = double.Parse(ratti_text_box.Text);

                        double pure_gold = total_weight / 96 * (96 - ratti);
                        pure_gold_textbox.Text = Math.Round(pure_gold, 2).ToString();

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
            if (num1 != null && num2 != null && num3 != null && numTotal != null)
            {
                if (num1.Text != "" && num2.Text != "" && num3.Text != "")
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
        private FlowDocument CreateFlowDocument()
        {
            // Create a FlowDocument  
            FlowDocument doc = new FlowDocument();
            // Create a Section  
            Section sec = new Section();
            // Create first Paragraph  
            Paragraph p1 = new Paragraph();
            // Create and add a new Bold, Italic and Underline  
            Bold bld = new Bold();
            bld.Inlines.Add(new Run("First Paragraph"));
            Italic italicBld = new Italic();
            italicBld.Inlines.Add(bld);
            Underline underlineItalicBld = new Underline();
            underlineItalicBld.Inlines.Add(italicBld);
            // Add Bold, Italic, Underline to Paragraph  
            p1.Inlines.Add(underlineItalicBld);
            // Add Paragraph to Section  
            sec.Blocks.Add(p1);
            // Add Section to FlowDocument  
            doc.Blocks.Add(sec);
            return doc;
        }




        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FlowDocument doc = new FlowDocument();  
            documentView docWin = new documentView(doc);
            docWin.Title = "Print";
            docWin.Width = 288;

            ScrollViewer sv = new ScrollViewer();

            Grid myGrid = myGridInit();

            // Heading: Row 0
            TextBlock heading = new TextBlock();
            heading.Text = "MW Gold Laboratory";
            heading.FontSize = 20;
            heading.FontWeight = FontWeights.Bold;
            heading.Height = 50;
            heading.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumnSpan(heading, 3);
            Grid.SetRow(heading, 0);
            myGrid.Children.Add(heading);

            //Date : Row 1
            TextBlock dateTxt = new TextBlock();
            dateTxt.FontSize = 16;
            dateTxt.FontWeight = FontWeights.Bold;
            dateTxt.Text = "Date: " + DateTime.Now;
            Grid.SetRow(dateTxt, 1);
            Grid.SetColumnSpan(dateTxt, 3);
            myGrid.Children.Add(dateTxt);

            //Gold Rate : Row 2
            TextBlock goldRateTxt = new TextBlock();
            goldRateTxt.FontSize = 16;
            goldRateTxt.FontWeight = FontWeights.Bold;
            goldRateTxt.Text = "Gold Rate: " + gold_rate_text_box.Text;
            Grid.SetRow(goldRateTxt, 2);
            Grid.SetColumnSpan(goldRateTxt, 3);
            myGrid.Children.Add(goldRateTxt);

          

            double totalPrice = 0;

            ObservableCollection<Item> dItems = new ObservableCollection<Item>();

            
            foreach (Item item in DataItem)
            {
                dItems.Add(new DisplayItem
                {
                    Name = item.name,

                    PureGold = item.pure_gold,
                    Ratti = item.ratti,
                    Gold_Weight = item.total_weight,

                });
            }

            DataGrid dataGrid = new DataGrid();

            dataGrid.ItemsSource = dItems;
            dataGrid.FontWeight = FontWeights.Bold;
            RowDefinition rowDef2 = new RowDefinition();
            myGrid.RowDefinitions.Add(rowDef2);
            Grid.SetRow(dataGrid, 3);
            Grid.SetColumnSpan(dataGrid, 3);
            dataGrid.DataContext = dItems;
            try{
                if (ckname.IsChecked == false && dataGrid.Columns[0] != null)
                {
                    dataGrid.Columns[0].Visibility = Visibility.Hidden;
                }
                if (ckratti.IsChecked == false && dataGrid.Columns[1] != null)
                {
                    dataGrid.Columns[1].Visibility = Visibility.Hidden;
                }
                if (cktWeight.IsChecked == false && dataGrid.Columns[2] != null)
                {
                    dataGrid.Columns[2].Visibility = Visibility.Hidden;
                }
                if (ckpGold.IsChecked == false && dataGrid.Columns[3] != null)
                {
                    dataGrid.Columns[3].Visibility = Visibility.Hidden;
                }
            }
            catch(Exception error)
            {
                MessageBox.Show(error.ToString());
            }
            MessageBox.Show( "Columns Count:"+dataGrid.Columns.Count.ToString());

           
            myGrid.Children.Add(dataGrid);
            myGrid.ShowGridLines = false;

            



            if (gold_rate_text_box.Text != "")
            {
                ObservableCollection<Cash> dCash = new ObservableCollection<Cash>();

                foreach (Item item in DataItem)
                {
                    dCash.Add(new Cash
                    {
                        Name = item.name,

                        Labor = item.labor,
                        Total = item.total
                    });
                    totalPrice += double.Parse(item.total);
                }

                DataGrid dataGrid2 = new DataGrid();
                dataGrid2.SetBinding(ItemsControl.ItemsSourceProperty, new System.Windows.Data.Binding { Source = dCash });
                
                dataGrid2.ItemsSource = dCash;
                dataGrid2.DataContext = dCash;
                dataGrid2.FontWeight = FontWeights.Bold;
                //dataGrid2.ColumnFromDisplayIndex(0).Visibility=Visibility.Hidden;
                if(ckname.IsChecked == true && dataGrid2.Columns[0] != null )
                {
                    dataGrid2.Columns[0].Visibility = Visibility.Hidden;
                }
                if (cklabor.IsChecked == true && dataGrid2.Columns[1] != null)
                {
                    dataGrid2.Columns[1].Visibility = Visibility.Hidden;
                }
                if (cktotal.IsChecked == true && dataGrid2.Columns[2] != null)
                {
                    dataGrid2.Columns[2].Visibility = Visibility.Hidden;
                }



                RowDefinition rowDef8 = new RowDefinition();
                myGrid.RowDefinitions.Add(rowDef8);
                Grid.SetRow(dataGrid2, 4);
                Grid.SetColumnSpan(dataGrid2, 3);
                myGrid.Children.Add(dataGrid2);
                myGrid.ShowGridLines = false;




            }

            foreach (var column in dataGrid.Columns)
            {
                Debug.WriteLine($"Column: {column.Header}, DisplayIndex: {column.DisplayIndex}");
            }

            RowDefinition rowDef7 = new RowDefinition();
            myGrid.RowDefinitions.Add(rowDef7);
            RowDefinition rowDef4 = new RowDefinition();
            myGrid.RowDefinitions.Add(rowDef4);


            TextBlock txt12 = new TextBlock();
            txt12.FontSize = 16;
            txt12.FontWeight = FontWeights.Bold;
            txt12.Text = "Total Price: " + totalPrice.ToString();
            Grid.SetRow(txt12, 5);
            Grid.SetColumnSpan(txt12, 3);
            myGrid.Children.Add(txt12);




            docWin.Content = myGrid;
            
            docWin.Show();
            PrintDialog printDlg = new PrintDialog();
            bool? print = printDlg.ShowDialog();
            if (print != null)
            {
                if ((print ?? false))
                {
                    printDlg.PrintVisual(myGrid, "receipt printing ..");
                    printDlg.PrintVisual(myGrid, "receipt printing ..");
                }

            }
        }

        private void setvisibility()
        {

        }
        private Grid myGridInit()
        {
            Grid myGrid = new Grid();

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
            RowDefinition rowDef3 = new RowDefinition();

            myGrid.RowDefinitions.Add(rowDef3);

            return myGrid;
        }

        private void gold_rate_text_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            calcTotal();
        }

        private void labor_text_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            //calcTotal();
        }

        void calcTotal()
        {
            if (gold_rate_text_box != null && total_text_box != null && labor_text_box != null && numTotal != null && pure_gold_textbox != null)
            {
                if (gold_rate_text_box.Text != "" && labor_text_box.Text != "" && pure_gold_textbox.Text != "")
                {
                    double rate = double.Parse(gold_rate_text_box.Text);
                    double labor = double.Parse(labor_text_box.Text);
                    double pure = double.Parse(pure_gold_textbox.Text);

                    double res = Math.Round(((rate / 11.664) * pure) - labor, 0);

                    total_text_box.Text = res.ToString();
                }
            }
        }

        private void pure_gold_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void total_weight_text_box_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.D)
            {
                img_GiveGold.Visibility = Visibility.Visible;
                img_TakeGold.Visibility = Visibility.Hidden;
            }
            else if(e.Key == Key.L)
            {
                img_TakeGold.Visibility = Visibility.Visible;
                img_GiveGold.Visibility = Visibility.Hidden;
            }
            validateInput(sender,e);
        }

        void validateInput(object sender,KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!((e.Key >= Key.D0 && e.Key <= Key.D9) ||
                (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                e.Key == Key.Back ||
                e.Key == Key.Delete ||
                e.Key == Key.Decimal ||
                e.Key == Key.Tab
                ))
            {
                // Suppress the key press if it's not a valid input
                e.Handled = true;
            }

            // Allow only one decimal point
            if (e.Key == Key.Decimal && textBox.Text.Contains("."))
            {
                // Suppress the key press if there's already a decimal point
                e.Handled = true;
            }
        }

        private void ratti_text_box_KeyDown(object sender, KeyEventArgs e)
        {
            validateInput(sender,e);

        }

        private void gold_rate_text_box_KeyDown(object sender, KeyEventArgs e)
        {
            validateInput(sender,e);

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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();


            //Create array of allowed column 

            Dictionary<string, bool> goldAllowedColumns = new Dictionary<string, bool>();

            goldAllowedColumns.Add("Name", ckname.IsChecked ?? true);
            goldAllowedColumns.Add("Ratti", ckratti.IsChecked ?? true);
            goldAllowedColumns.Add("Gold", cktWeight.IsChecked ?? true);
            goldAllowedColumns.Add("Pure", ckpGold.IsChecked ?? true);

            Dictionary<string, bool> cashAllowedColumns = new Dictionary<string, bool>();

            cashAllowedColumns.Add("Name", ckname.IsChecked ?? true);
            cashAllowedColumns.Add("Labor", cklabor.IsChecked ?? true);
            cashAllowedColumns.Add("Total", cktotal.IsChecked ?? true);



        // Create a FlowDocument dynamically.  
        FlowDocument doc = CreateFlowDocument(DateTime.Now, goldAllowedColumns, cashAllowedColumns);
            doc.Name = "FlowDoc";
            // Create IDocumentPaginatorSource from FlowDocument  
            IDocumentPaginatorSource idpSource = doc;
            // Call PrintDocument method to send document to printer  
            documentView dwin=new documentView(doc); 
            dwin.Width = 288;
            //dwin.Show();
           

            bool? print = printDlg.ShowDialog();
            if (print != null)
            {
                if ((print ?? false))
                {
                    printDlg.PrintDocument(idpSource.DocumentPaginator, "Printing Reciept.");
                    printDlg.PrintDocument(idpSource.DocumentPaginator, "Printing.");
                }

            }
        }
        public FlowDocument CreateFlowDocument(DateTime date, Dictionary<string, bool> goldAC,Dictionary<string,bool> cashAC)
        {
            // Create a new flow document
            FlowDocument flowDoc = new FlowDocument();
            int pageWidth = 288;
            flowDoc.PageWidth = pageWidth;
            flowDoc.PagePadding = new Thickness(0);

            // Create a paragraph for the heading and set its properties
            Paragraph heading = new Paragraph(new Run("MW Gold Lab"));
            heading.TextAlignment = TextAlignment.Center;
            heading.FontSize = 24;
            heading.FontWeight = FontWeights.Bold;

            // Add the heading to the flow document
            flowDoc.Blocks.Add(heading);

            // Create a paragraph for the subheading and set its properties
            Paragraph subheading = new Paragraph(new Run(date.ToString("G")));
            subheading.TextAlignment = TextAlignment.Center;
            subheading.FontSize = 18;
            subheading.FontStyle = FontStyles.Italic;

            // Add the subheading to the flow document
            flowDoc.Blocks.Add(subheading);

            if (DataItem != null)
            {
                Paragraph goldRateParagraph = new Paragraph(new Run("Gold Rate: " + DataItem[0].gold_rate.ToString()));
                goldRateParagraph.TextAlignment = TextAlignment.Left;
                goldRateParagraph.FontSize = 16;
                goldRateParagraph.FontWeight = FontWeights.Bold;
                goldRateParagraph.Margin = new Thickness(0, 10, 0, 10);

                // Add the total paragraph to the flow document
                flowDoc.Blocks.Add(goldRateParagraph); 
            }



            // Create a table and set its properties
            Table table = new Table();
            table.CellSpacing = 10;
            table.BorderBrush = Brushes.Black;
            table.BorderThickness = new Thickness(1);
            table.Margin = new Thickness(2, 10, 2, 10);

            // Generate required columns for the table
            //int noOfAllowedColumns = 0;

            //foreach(bool allowedColumn in allowedColumns.Values)
            //{
            //    if (allowedColumn)
            //    {
            //        noOfAllowedColumns++;
            //    }
            //}  
            int gColCount = goldAC.Count(e => e.Value == true);
            



            if (goldAC["Name"] == true)
            {
                table.Columns.Add(new TableColumn());

            }
            if (goldAC["Gold"] == true)
            {
                table.Columns.Add(new TableColumn() { Width = new GridLength(((pageWidth / gColCount) * 0.8)) });

            }
            if (goldAC["Ratti"] == true)
            {
                table.Columns.Add(new TableColumn() { Width = new GridLength(((pageWidth / gColCount) * 0.8)) });

            }
            if (goldAC["Pure"] == true)
            {
                table.Columns.Add(new TableColumn() { Width = new GridLength(((pageWidth / gColCount) * 0.3)) });
                table.Columns.Add(new TableColumn() { Width = new GridLength(((pageWidth / gColCount) * 0.8)) });


            }
        
            

          
            // Create a row group for the table
            TableRowGroup rowGroup = new TableRowGroup();

            // Create a header row for the table and set its properties
            TableRow headerRow = new TableRow();
            headerRow.Background = Brushes.LightGray;
            headerRow.FontWeight = FontWeights.Bold;
            headerRow.FontSize = 11;

            // Generata Header cells according to data

            if (goldAC["Name"]==true)
            {
                headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Name"))));
            }
            if (goldAC["Gold"]==true)
            {
                headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Gold"))));
            }
            if (goldAC["Ratti"]==true)
            {
                headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Ratti"))));
            }
            if (goldAC["Pure"]==true)
            {
                headerRow.Cells.Add(new TableCell(new Paragraph(new Run("L/D"))));
                headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Pure"))));
            }

           
            
            // Add the header row to the row group
            rowGroup.Rows.Add(headerRow);



            // Declare a variable to store the total sum of all products
            double total = 0;

            // Loop through the DataItem  and create a row for each Item
            foreach(Item item in DataItem)
            {
                // Create a new row for the product
                TableRow productRow = new TableRow();
                productRow.FontSize = 16;
                productRow.FontFamily = new FontFamily("Times New Roman");
                // Generata cells according to data
                if (goldAC["Name"]==true)
                {
                    productRow.Cells.Add(new TableCell(new Paragraph(new Run(item.name))));
                }
                if (goldAC["Gold"]==true)
                {
                    productRow.Cells.Add(new TableCell(new Paragraph(new Run(item.total_weight))));
                }

                if (goldAC["Ratti"]==true)
                {
                    productRow.Cells.Add(new TableCell(new Paragraph(new Run(item.ratti))));
                }
                if (goldAC["Pure"]==true)
                {
                    productRow.Cells.Add(new TableCell(new Paragraph(new Run(item.goldStatus))));
                    productRow.Cells.Add(new TableCell(new Paragraph(new Run(item.pure_gold))));
                }
               


                // Add the product row to the row group
                rowGroup.Rows.Add(productRow);

                // Update the total sum by multiplying the price and quantity of the product
                total += double.Parse(item.total);
            }



            

            // Add the row group to the table
            table.RowGroups.Add(rowGroup);

            // Add the table to the flow document
            flowDoc.Blocks.Add(table);

            // Create a table and set its properties
            Table table2 = new Table();
            table2.CellSpacing = 10;
            table2.BorderBrush = Brushes.Black;
            table2.BorderThickness = new Thickness(1);
            table2.Margin = new Thickness(2, 10, 2, 10);
            


            if (cashAC["Name"] == true)
            {
                table2.Columns.Add(new TableColumn());

            }
            if (cashAC["Labor"] == true)
            {
                table2.Columns.Add(new TableColumn() { Width = new GridLength(((pageWidth / gColCount) * 0.8)) });

            }
            if (cashAC["Total"] == true)
            {

                table2.Columns.Add(new TableColumn() { Width = new GridLength(((pageWidth / gColCount) * 0.3)) });
                table2.Columns.Add(new TableColumn() { Width = new GridLength(((pageWidth / gColCount) * 0.8)) });

            }


            // Create a row group for the table2
            TableRowGroup rowGroup2 = new TableRowGroup();

            // Create a header row for the table2 and set its properties
            TableRow headerRow2 = new TableRow();
            headerRow2.Background = Brushes.LightGray;
            headerRow2.FontWeight = FontWeights.Bold;
            headerRow2.FontSize = 11;

            // Generata Header cells according to data

            if (cashAC["Name"] == true)
            {
                headerRow2.Cells.Add(new TableCell(new Paragraph(new Run("Name"))));
            }
            if (cashAC["Labor"] == true)
            {
                headerRow2.Cells.Add(new TableCell(new Paragraph(new Run("Labor"))));
            }
            if (cashAC["Total"] == true)
            {
                headerRow2.Cells.Add(new TableCell(new Paragraph(new Run("L/D"))));
                headerRow2.Cells.Add(new TableCell(new Paragraph(new Run("Total"))));
            }

            // Add the header row to the row group
            rowGroup2.Rows.Add(headerRow2);



            // Declare a variable to store the total sum of all products

            // Loop through the DataItem  and create a row for each Item
            foreach (Item item in DataItem)
            {
                // Create a new row for the product
                TableRow productRow = new TableRow();
                productRow.FontSize = 16;
                productRow.FontFamily = new FontFamily("Times New Roman");
                // Generata cells according to data
                if (cashAC["Name"] == true)
                {
                    productRow.Cells.Add(new TableCell(new Paragraph(new Run(item.name))));
                }
                if (cashAC["Labor"] == true)
                {
                    productRow.Cells.Add(new TableCell(new Paragraph(new Run(item.labor))));
                }
                if (cashAC["Total"] == true)
                {
                    productRow.Cells.Add(new TableCell(new Paragraph(new Run(item.cashStatus))));
                    productRow.Cells.Add(new TableCell(new Paragraph(new Run(item.total))));
                }

                // Add the product row to the row group
                rowGroup2.Rows.Add(productRow);

                // Update the total sum by multiplying the price and quantity of the product
                total += double.Parse(item.total);
            }





            // Add the row group to the table2
            table2.RowGroups.Add(rowGroup2);

            // Add the table to the flow document
            flowDoc.Blocks.Add(table2);

            // Create a paragraph for the total sum and set its properties
            Paragraph totalParagraph = new Paragraph(new Run("Total Amount: " + total.ToString("C")));
            totalParagraph.TextAlignment = TextAlignment.Right;
            totalParagraph.FontSize = 16;
            totalParagraph.FontWeight = FontWeights.Bold;



            // Add the total paragraph to the flow document
            flowDoc.Blocks.Add(totalParagraph);

            // Return the flow document
            return flowDoc;
        }

        private void total_text_box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D)
            {
                img_Takemoney.Visibility = Visibility.Hidden;
                img_Givemoney.Visibility = Visibility.Visible;
            }
            else if(e.Key == Key.L)
            {
                img_Takemoney.Visibility = Visibility.Visible;
                img_Givemoney.Visibility = Visibility.Hidden;
            }
            if(e.Key == Key.Enter)
            {
                if (IsInitialized)
                {

                    if (gold_rate_text_box.Text != "")
                    {
                        double rate = double.Parse(gold_rate_text_box.Text);
                        double pureGold = ((double.Parse(total_text_box.Text) / double.Parse(gold_rate_text_box.Text)) * 11.664);
                        pure_gold_textbox.Text = Math.Round(pureGold,2).ToString();

                    }
                }
            }
            validateInput(sender,e);
        }

        private void total_text_box_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void total_text_box_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            DataItem.Clear();
        }

        private void items_list_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                if(items_list.SelectedItem != null)
                    DataItem.Remove((Item)items_list.SelectedItem);
            }
        }
    }

       
    }
