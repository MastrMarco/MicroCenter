   M S B A M L   `   `   `  ���� F  BMicroCenter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:MicroCenter.PagineMicroCenter.Pagine  $    MicroCenter.Pagine.Connessione   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     2local clr-namespace:MicroCenter.Pagine       N� Title$  Connessione��.����    SecundaryBackgroundColor#���    �� CornerRadius$ 10��._��  � ColumnDefinitions �� $	��250 ��� $	��450 ��� $	��235 �.+�� $��0��.+��� $ 8��$��
10,10,0,10q�._��  � RowDefinitions �� $	��160 ��� $��* �.+��� $ 8��$	��230��$	��150��  TertiaryBackgroundColor#���  $��Center=�$	��Top=� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 8Q�   ShadowDepth$ 0Q�   Color$ #5B8DEF���� $ 8��$	��230��$	��150��#���  $��Center=�$	��top=�._�� $��0�� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� $��0��.+��� $ 8��$	��230��$
��36.6��#���  $��Center=�$	��Top=�   $ 8Q�$ 0Q�$ #5B8DEF���� -   $��LabTinfoSoft��$��Dispositivo��$��20(�$
��Bold"�  PrimaryTextColor#���  $��Center=�$	��Top=�� $��1��.+��� -   $��LabNomeDispositivo��$
��Nome��$��18(�$
��Bold"�#���  $��Center=�$	��Top=�� $��2��.+���  �� Orientation$ 
Horizontal=�$��Center=�$
��Auto��$
��Auto��.+��� -   $��	LaVerSoft��$
��----��$��16(�$
��Bold"�#���  $��Center=�$��Center=�$��	10 0 10 0q��� -   $��	LaVerTipo��$��-��$��16(�$
��Bold"�#���  $��Center=�$��Center=�$��	10 0 10 0q�� $��3��.+��� $ 
Horizontal=�$��Center=�$
��Auto��$
��Auto��.+��� -   $��LaTempScheda��$
��----��$��16(�$
��Bold"�#���  $��Center=�$��Center=�$��	10 0 10 0q��� -   $��LaVref��$
��----��$��16(�$
��Bold"�#���  $��Center=�$��Center=�$��	10 0 10 0q�� $��4��.+��� -   $��LaSoC��$��	---------��$��16(�$
��Bold"�#���  $��Center=�$��Center=�� $��1��.+��� #���  $ 8��$	��230��$	��270��._��� $ 16��$	��150��$	��150��#���  $��Center=�$��Center=�� $��1��.+��� $ 8��#���  $��10q�   $ 4Q�$ 0Q�$ #5B8DEF���� #���  $ 8��$��10q�._�� 	 � Cursor$	  �� �� $	��170 ��� $��95 ��� $��* �.+�� $��0��$	��410��$	��150��$��10q�.+��� $ 8��$	��410��$	��150��#���  $��Center=�$	��Top=�   $ 8Q�$ 0Q�$ #5B8DEF��� $	  �� �� $
��Auto ��� $��* �.+�� $��0��.+��� $ 8��$	��410��$
��36.6��#���  $��Center=�$	��Top=�   $ 16Q�$ 0Q�$ #5B8DEF���� -   $��LabTSerialPort��$��Seleziona Dispositivo��$��20(�$
��Bold"�#���  $��Center=�$	��Top=�� $��1�� �� $��* ��� $��* �.+��� -	   
 ��Name$
 SerialPortComboBox��  ModernComboBoxStyle#��[   ModernComboBoxItemStyle#��[ #���  #���  $��40��$	��150���� -
    ��Name$ btnConnetti��$��1��$��50��$��50��  IconButtonsForms#��[   IconaUSBConnetti#���  $
	 Hand�� � ToolTip �� -   $��LabToolTipConnetti��$��Disconnesso��� $��1�� �� $	��230 ��� $��* �.+�� $��0��$��10q�$	��200��$��70��.+��� $ 8��$	��200��$��70��#���  $��Center=�$	��Top=�   $ 8Q�$ 0Q�$ #5B8DEF���  �� $
��Auto ��� $
��Auto �.+�� $��0��.+��� $ 8��$	��200��$��31.28��#���  $��Center=�$	��Top=�   $ 16Q�$ 0Q�$ #5B8DEF���� -   $��LabTConnessione��$��Stato Collegamento��$��16(�$
��Bold"�#���  $��Center=�$	��Top=�� $��1��.+��� -   $��LaConnessione��$��Disconnesso��$��18(�$
��Bold"�#���  $��Center=�$��Center=�� $��1��$	��100��$��70��$��10q�.+��� $ 8��$	��100��$��70��#���  $��Center=�$��Center=�   $ 8Q�$ 0Q�$ #5B8DEF��� $��2��$��10q�.+��� $ 8��$	��410��$	��130��#���  $��Center=�$	��Top=�   $ 8Q�$ 0Q�$ #5B8DEF���  �� $
��Auto ��� $
��Auto �.+�� $��0��.+��� $ 8��$	��410��$��24��#���  $��Center=�$	��Top=�   $ 8Q�$ 0Q�$ #5B8DEF���� -   $ btnInfoSeriale��$��1��  IconButtonsStyle#��[   Informazioni#���  $
	 Hand��$
��Left=�$��Center=�$��18(��� -   $��LaTSerialData��$��Dati Seriali��$��18(�$
��Bold"�#���  $��Center=�$��Center=�� $��1��$	��410��$
��Auto��.+��� -   $��TxSerialText��$��
SerialPort��$��14(�$
��Bold"�#���  $��Center=�$��Center=�$
��Wrap=�$��5q�� $��2�� �� $	��13* ��� $
��437* �.+��� $ 8��$��
0,10,10,10q�$��2��._��  �� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+�� $��0��$	��200��$��70��.+��� $ 8��$	��200��$��70��#���  $��Center=�$��Center=� J� BitmapEffect F�  F� ShadowDepth$ 0Q� F� 	Direction$ 0Q� F� Color$ #5B8DEF�� F� Softness$	 0.5Q��� -   $��La��$��Dati Seriali��$��18(�$
��Bold"�#���  $��Center=�$��Center=�� $��1��$	��200��$��70��.+��� $ 8��$	��200��$��70��#���  $��Center=�$��Center=� F� $ 0Q�$ 0Q�$ #5B8DEF��$	 0.5Q��� -   $	��La1��$��Dati Seriali��$��18(�$
��Bold"�#���  $��Center=�$��Center=�� $��2��$	��200��$��70��.+��� $ 8��$	��200��$��70��#���  $��Center=�$��Center=� F� $ 0Q�$ 0Q�$ #5B8DEF��$	 0.5Q��� -   $	��La2��$��Dati Seriali��$��18(�$
��Bold"�#���  $��Center=�$��Center=�� $��3��$	��200��$��70��.+��� $ 8��$	��200��$��70��#���  $��Center=�$��Center=� F� $ 0Q�$ 0Q�$ #5B8DEF��$	 0.5Q��� -   $	��La3��$��Dati Seriali��$��18(�$
��Bold"�#���  $��Center=�$��Center=�� $��4��$	��200��$��70��.+��� $ 8��$	��200��$��70��#���  $��Center=�$��Center=� F� $ 0Q�$ 0Q�$ #5B8DEF��$	 0.5Q��� -   $	��La4��$��Dati Seriali��$��18(�$
��Bold"�#���  $��Center=�$��Center=�