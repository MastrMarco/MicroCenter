   M S B A M L   `   `   `  ���� F  BMicroCenter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:MicroCenter.Pagine.HubPCMicroCenter.Pagine.HubPC  o<clr-namespace:MicroCenter.Stile_Elementi.CircularProgressBar.MicroCenter.Stile_Elementi.CircularProgressBar  '    !MicroCenter.Pagine.HubPC.Elementi   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8local&clr-namespace:MicroCenter.Pagine.HubPC     Lcpb<clr-namespace:MicroCenter.Stile_Elementi.CircularProgressBar       N� Title$  Elementi��.����    SecundaryBackgroundColor#���    �� CornerRadius$ 10��._�� .+��� -    ��Name$ Altro��$ 8��$
��Auto��$��35��$	��top=�$��10q�  TertiaryBackgroundColor#���  �� -   $ InfoElemento��$ 8��$	��555��$	��250��$	��top=�$
��Left=�$��10,55,10,10q�#���  ._��  � ColumnDefinitions �� $��* ��� $��2* �.+�� $��0��� $��1�� � RowDefinitions �� $
��Auto ��� $��4* ��� $��* �.+��� -    ��Name$ LaNomeElemento��$��0��$	��HUB��$��Center=�$��5q�$��16(�$
��Bold"�  PrimaryTextColor#���  � $��1�� �� $��* ��� $��* �.+�� -    �Name$ LaDatiElemento��$��0��.+��� -    ��Name$ ListLaDatiElemento�� �� Orientation$ Vertical=�$
��Left=�$	��Top=�$��20,0,0,0q�� -   $ DatiElemento��$��1��.+��� -   $ ListLaDatiElementov��$ Vertical=�$��Right=�$	��Top=�$��0,0,20,0q�� $��2��.+��� -   $ ListSelectElemento��$ 
Horizontal=�$��Center=�$��Center=��� -	   $ TabInfo��$ 8��$	��100��$	��250��$	��top=�$
��Left=�$��575,55,10,10q�#���  ._�� .+�H   BMicroCenter.Stile_Elementi.CircularProgressBar.CircularProgressBar  -
   	  Name$	 Br_Temperatura��
   	Thickness$
 10Q�   Radius$ 34Q� � Value$	 100Q�  CircularProgressBarStyle#��[   PrimaryTealColor#���     Fill
 ����   PrimaryBackgroundColor   Stroke# �     StrokeThickness$ 0Q�   
StrokeMode$ Middle=�   
EndLineCap$ Round=�$��0 0 0 0q��� -   $ ModificaElemento��$ 8��$	��220��$	��385��$	��top=�$��Right=�$��10,55,10,10q�#���  ._��� -   $ StackPanelPanelloModifica��$ Vertical=�$��Center=�$	��top=�.+��� -   $ 	TabColore��$ 8��$	��200��$	��100��$	��top=�$��Center=�$��10q�#���   ._�� $	��200��.+��� -   $ LaColore��$��Seleziona Colore��$��16(�$
��Bold"�$��Center=�$	��Top=�$��2q�#���  �� -   $ Color0��$ 
Horizontal=�$��Center=�$	��top=�$��0,25,0,0q��� -   $ Color1��$ 
Horizontal=�$��Center=�$	��top=�$��0,55,0,0q��� -   $ 
TabVentole��$ 8��$	��200��$��50��$	��top=�$��Center=�$��
10,0,10,10q�#���   ._�� $	��200�� �� $
��Auto ��� $��* �.+��� -   $ 	LaVentole��$��0��$��Velocità Ventole��$��16(�$
��Bold"�$��Center=�$	��Top=�$��2q�#���  �� -    ��Name$ TrackVelocità��$��1��$��Center=�$	��top=�$��0q�$	��150��  AppSliderStyle#���   � Minimum$ 5Q� � Maximum$	 255Q� � LargeChange$ 1Q�$	 128Q��� -   $ TabLuminosità��$ 8��$	��200��$��50��$	��top=�$��Center=�$��	10,0,10,0q�#���   ._�� $	��200�� �� $
��Auto ��� $��* �.+��� -   $ LaLuminosità��$��Luminosità��$��16(�$
��Bold"�$��Center=�$	��Top=�$��2q�#���  �� -   $ TrackLuminosità��$��1��$��Center=�$	��top=�$��0q�$	��150��#���  $ 5Q�$	 255Q�$ 1Q�$	 128Q��� -   $ TabFunzioni_LED��$ 8��$	��200��$	��135��$	��top=�$��Center=�$��10q�#���   ._�� $	��200�� �� $��25 ��� $��* ��� $��* �.+��� -   $ LaFunzioni_LED��$��0��$��Animazioni RGB��$��16(�$
��Bold"�$��Center=�$	��Top=�$��2q�#���  �� -   $ Animazione0��$��1��$ 
Horizontal=�$��Center=�$��Center=�$��0,0,0,0q��� -   $ Animazione1��$��2��$ 
Horizontal=�$��Center=�$��Center=�$��0,0,0,0q��� -   $ ListaElementi��$ 8��$	��665��$	��125��$��Bottom=�$
��Left=�$��10,10,10,10q�#���  ._�� .+��� -   $ StackPanelContainer��$ 
Horizontal=�$��Center=�$��Center=�