   M S B A M L   `   `   `  ���� F  BMicroCenter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:MicroCenter.PagineMicroCenter.Pagine  $    MicroCenter.Pagine.Connessione   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     2local clr-namespace:MicroCenter.Pagine       N� Title$  Connessione��.����    SecundaryBackgroundColor#���    �� CornerRadius$ 10��._��  � ColumnDefinitions �� $	��250 ��� $	��450 ��� $	��235 �.+�� $��0��.+��� $ 8��$��
10,10,0,10q�._��  � RowDefinitions �� $	��160 ��� $��* �.+��� $ 8��$	��230��$	��150��  TertiaryBackgroundColor#���  $��Center=�$	��top=�._�� $��0�� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� $��0��.+��� -   $��LabTinfoSoft��$��Dispositivo��$��20(�$
��Bold"�  PrimaryTextColor#���  $��Center=�$	��Top=�� $��1��.+��� -   $��LabNomeDispositivo��$
��Nome��$��18(�$
��Bold"�#���  $��Center=�$	��Top=�� $��2��.+���  �� Orientation$ 
Horizontal=�$��Center=�$
��Auto��$
��Auto��.+��� -   $��	LaVerSoft��$
��----��$��16(�$
��Bold"�#���  $��Center=�$��Center=�$��	10 0 10 0q��� -   $��	LaVerTipo��$��-��$��16(�$
��Bold"�#���  $��Center=�$��Center=�$��	10 0 10 0q�� $��3��.+��� $ 
Horizontal=�$��Center=�$
��Auto��$
��Auto��.+��� -   $��LaTempScheda��$
��----��$��16(�$
��Bold"�#���  $��Center=�$��Center=�$��	10 0 10 0q��� -   $��LaVref��$
��----��$��16(�$
��Bold"�#���  $��Center=�$��Center=�$��	10 0 10 0q�� $��4��.+��� -   $��LaSoC��$��	---------��$��16(�$
��Bold"�#���  $��Center=�$��Center=�� $��1��.+��� #���  $ 8��$	��230��$	��270��._��� $ 16��$	��150��$	��150��#���  $��Center=�$��Center=�� $��1��.+��� $ 8��#���  $��10q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 4Q�   ShadowDepth$ 0Q�   Color$ #5B8DEF���� #���  $ 8��$��10q�._��  �� $��* ��� $��* �.+��� $��0��$ 8��$��10q�$	��410��$	��150��#���  $��Center=�$	��Top=�   $ 8Q�$ 0Q�$ #5B8DEF���� $��0��$ 8��$��10q�$	��410��$	��150��#���  $��Center=�$	��top=�._�� $��0�� �� $
��Auto ��� $��* �.+��� $��0��$ 8��$	��410��$
��36.6��#���  $��Center=�$	��Top=�   $ 16Q�$ 0Q�$ #5B8DEF���� $��0��$ 8��$	��410��$
��Auto��#���  $��Center=�$	��Top=�._��� -   $��LabTSerialPort��$��Seleziona Dispositivo��$��20(�$
��Bold"�#���  $��Center=�$	��Top=�� $��1�� �� $��* ��� $��* �.+��� -	   	 ��Name$	 SerialPortComboBox��  ModernComboBoxStyle#��[   ModernComboBoxItemStyle#��[ $��40��$	��150���� -
   
 ��Name$
 btnConnetti��$��1��  IconButtonsForms#��[   Arduino#���   � ToolTip �� -   $��LabToolTipConnetti��$	��C/N��� $��1��.+��� #���  $ 8��._��� $ 16��$��	0,14,16,0q�$	��400��$	��150��#���  $��Right=�$	��Top=�._��� -   $��TxSerialText��$��1��$��
SerialPort��$��16(�$
��Bold"�#���  $��Center=�$��Center=�� $��2��.+��� $ 8��$��
0,10,10,10q�._��  �� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� $��0��$ 8��$	��205��$��70��#���  $��Center=�$	��top=��� $��1��$ 8��$	��205��$��70��#���  $��Center=�$	��top=��� $��2��$ 8��$	��205��$��70��#���  $��Center=�$	��top=��� $��3��$ 8��$	��205��$��70��#���  $��Center=�$	��top=��� $��4��$ 8��$	��205��$��70��#���  $��Center=�$	��top=�