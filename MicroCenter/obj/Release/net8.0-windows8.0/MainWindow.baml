   M S B A M L   `   `   `  ���� F  BMicroCenter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MicroCenterMicroCenter      MicroCenter.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:MicroCenter     -     � Title$  MicroCenter��$	��550��$
��1020�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=�
������  � AllowsTransparency$
 True�� � 
ResizeMode$ NoResize=� � WindowState$ Normal=�.���� -    ��Name$ ContenitoreForm�� �� CornerRadius$ 10��   PrimaryBackgroundColor#���    J� Opacity$ 1Q�._�� .+��� -   $ BarraAppList��$ 10��	 �� Padding$	 0,55,0,0q�  SecundaryBackgroundColor#���  $��60��$
��Left=�._�� 
 � RowDefinitions
 �� $��60 ��� $��60 ��� $��60 ��� $��* ��� $��45 �.+��� -    ��Name$ btnImpostazioni��$��0��  IconButtonsForms#��[   Impostazioni#���   � ToolTip �� -   $��LabToolImpostazioni��$��Impostazioni���� -   $ btnConnessione��$��1��#��[   Connessione#���   �� -   $��LabToolTipConnessione��$��Connetti���� -   $ 
btnProject��$��2��#��[   Arduino#���   �� -	   $��LabToolTipArduino��$��Arduino���� -
   $ BarraInferirore��$ 10��#���  $��35��$	��940��$��Bottom=�$��Center=�$��60,0,0,9q�._��  � ColumnDefinitions �� $��40 ��� $��* ��� $��60 �.+�F� $��0�� �� Stretch$ Uniform=�$��Center=�  PrimaryBlueColor#���  $��30��  	IconaSoft#���  �� -   $��LabInfoPagine��$��1��$��Impostazioni��$��16(�$
��Bold"�  PrimaryTextColor#���  $��Center=��� #���  $	��935��$	��450��$��Center=�$��Center=�$��60,0,0,5q�$ 10��._� � -     �Name$ frameContent��  � NavigationUIVisibility$ Hidden=�$��	10,0,10,0q��� -   $ BarraSuperiore��$ 10��$	 0q� 	 TertiaryBackgroundColor#��� 	 $��35��$	��Top=�._��  �� $��60 ��� $��* ��� $��35 ��� $��35 �.+�F� $ Uniform=�$��0,0,0,0q�$��Center=�#���  $��20��#���  �� -   $ btnClose��$��3�� 
 IconButtonsStyle#��[
  
 Chiudi#���  
 � Tag$ IsCloseButton���� -   $ btnNascondi��$��2��#��[
   Nascondi#���  $ IsButtonBarTop��