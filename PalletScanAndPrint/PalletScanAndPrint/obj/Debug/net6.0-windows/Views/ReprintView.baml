   M S B A M L   `   `   `  ????M  IPalletScanAndPrint, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:PalletScanAndPrint.ViewsPalletScanAndPrint.Views  5      *    $PalletScanAndPrint.Views.ReprintView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:PalletScanAndPrint.Views     5      .??5
      ? 
????  ?$
??1080??6!   $
??1920??6/     ? RowDefinitions  5   
   ?? 5      $	??70* ?6   ?? 5      $	??70* ?6   ?? 5      $	??70* ?6   ?? 5      $	??70* ?6   ?? 5      $	??70* ?6   ?? 5      $	??70* ?6   ?? 5      $	??70* ?6   ?? 5      $	??70* ?6   ?? 5      $	??70* ?6   ?? 5      $	??70* ?6   ?? 5      $	??70* ?6   ?? 5      $	??70* ?6   ?? 5      $	??70* ?6   ?? 5      $
??100* ?6    ? ColumnDefinitions 5   
   ?? 5      $
??140* ?6   ?? 5      $
??190* ?6   ?? 5      $
??240* ?6   ?? 5       $
??240* ?6   ?? 5!      $
??240* ?6   ?? 5"      $
??240* ?6   ?? 5#      $
??180* ?6   ?? 5$      $
??140* ?6   .+?5)   
   &   System.Windows.Controls.DataGrid  -   5        $??CustomerGrid??5)      $??8??5*      $??13??5+      $??45(?5,         RowBackground
 ?????   AlternatingRowBackground
 ??ح? ?? AlternationCount$ 2??50         CanUserAddRows$ False??51         
IsReadOnly$
 True??52      $??120,30,136,56q?53         CanUserResizeColumns$ False??54         SelectionMode$ Single=?55      
????ح?	   AutoGenerateColumns$	 False??59      ??5-      ?? *
LabelsInfo+
 ?? UpdateSourceTrigger$
 PropertyChanged=? ?? SelectedItem 58      ?? *SelectedRow+   Columns 5;      0  *System.Windows.Controls.DataGridTextColumn  5<      ,  &System.Windows.Controls.DataGridColumn   Header$ Client??6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 204 5>   %   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5=   %   ?? *
Customer+5>   %     5?      $ 	Nr. Palet??6%   $	 294 5A   %    5@   %   ?? *PalletNumber+5A   %     5B      $ Specificatii??6%   $	 320 5D   %    5C   %   ?? *Specification+5D   %     5E      $ Model??6%   $	 320 5G   %    5F   %   ?? *Series+5G   %     5H      $ Prima Serie??6%   $	 250 5J   %    5I   %   ?? *SeriesStart+5J   %     5K      $ Ultima Serie??6%   $	 250 5M   %    5L   %   ?? *	SeriesEnd+5M   %   5P      ?? 5U   
   -   5         ??Name$ 	btnPrint1??5Y      $??12??5U      $??2??5V      $??1??5W      $??2??5X      
??? ? ?
???   ?$??Verdana)?5\      $??44(?5]      $??Italic$?5^      $
??Bold"?5_      $??20,50,230,40q?5a      .??6'   	Printeaza ? 	Resources 5c       ?? 
TargetType ?? Property ?? Value??%_   '??       ?? 5d      " ??6   .?5e      ??  ?@??CornerRadius$ 20??65   5f      5g      5h      ?? 5l   
   $??12??6   $??2??5m      $??3??5n      $??2??5o      
????  ?$??3,3,3,3q?5q      
???????$??	0,40,0,30q?5s      ._?5t      ?? 
???????
???   ?$??44(?5v       ?? TextAlignment$ Center=?5x      $??Center=?5y      $
??Wrap=?5z      $??	20,0,20,0q?5{      ??5w      ?? *Status+$
 PropertyChanged=?5{       5|      ??%_   '??       ?? 5}      " ??6   .?5~      ??  ?@??CornerRadius$ 20??65   5      5?      5?      ?? 5?   
   -   5        $??12??5?      $??2??5?      $??6??5?      $??Meniu??5?      
????? ?$??42(?5?      $??22,50,22,40q?5?       5?      ??%_   '??       ?? 5?      " ??6   .?5?      ??  ?@??CornerRadius$ 20??65   5?      5?      5?      5?      5?      