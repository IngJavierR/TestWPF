   M S B A M L   `   `   `  ����E  ATestingWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:TestingWPF.ResourcesTestingWPF.Resources  5   	       TestingWPF.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   1my"clr-namespace:TestingWPF.Resources     5   	     � Title$  
MainWindow��5   	   $	��700��6   $	��725��6)    � 	Resources 5      .   (TestingWPF.Resources.ConvertStringToHour   	formatter��%   &	          5   
   6!   5      .��5	      �  � RowDefinitions 5
   
   �� 5      $��60 �6   6*   �� 5      $��40 �6   6*   �� 5      $��60 �6   6*   �� 5      $	��110 �6   6+   �� 5      $	��110 �6   6+   �� 5      $��60 �6   6*   �� 5      $	��100 �6   6+   �� 5      $��* �6   6)   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��50��6.   $	��Top=�6:   $��0��6R   .+�5      �� $��Images/samsung.jpg��6   $��30��60   $��80��6<   �� 5      -   5         ��Name$ Nombre��5      $��45��6(   	Nombre:6=   �� 5      -   5         ��Name$ 	NombreTxt��5        textBoxStyle#��[ 6)   �� 5      -   5        $ Apellido��5      
Apellido64   �� 5      -   5        $ ApellidoTxt��5        textBoxStyleBlack#��[ 6+   5      �� 5   
   $ 
Horizontal=�6   $��Center=�6.   $��50��6K   $	��200��6W   $��1��6c   .+�5      �� -   5        $��
buttonTest��5      $��20��6B   $��80��6N   ��5      �� 5      .+�5      �� -   5        $ 
buttonName��5   $   6:   5       5"      �� 5#      -   5        $��buttonConsulta��5#      $��20��6M   $��80��6Y   $��	Open Word��6d   6z   5$      �� 5%   
   -   5        $ 
Horizontal=�5%      $��2��6Q   .+�5&      �� -	   5         ��Name$ btn_si��5&      $	��100��6%   $��30��61   .��6=   Si6A   �� 5'      -
   5        $ btn_no��5'      $	��100��6%   $��30��61   .��6=   No6A   5(      �� 5)   
   $ Vertical=�6   $��3��6,   .+�5*      �� $	��Cut��6   $��20��6#   $��50��6/   ��6:   ��  �� ElementName$ txtDocument��.��6l   Cut6q   �� 5+      $
��Copy��6   $��20��6$   $��50��60   ��6;   �� $ txtDocument��.��6m   Copy6s   �� 5,      $��Paste��6   $��20��6%   $��50��61   ��6<   �� $ txtDocument��.��6n   Paste6u   �� 5-      -   5        $ txtDocument��5-      $	��300��6+   5.      �� 5/   
   -   5         ��Name$ PersonStack��5/      $ 
Horizontal=�6*   $��4��6C   .+�50      �� Name:6    �� 51      ��6   �� *Name+�� 52      Age:6   �� 53      ��6   �� *Age+�� 54      -   5        $ birthdayButton��54      .��6-   
Birthday67   55      �� 56   
   -   5        $ ComboBoxStack��56      $ 
Horizontal=�6,   $��5��6E   .+�57      �� -   5        	 ��Name$	 comboBoxDeportes��57      $��30��6\   $	��250��6h   
 �� SelectedValue
 61   �� *deporte_practico+6h   �� 58      -   5        $ btn_CreateExcel��58      6.   59      �� 5:   
   $��6��6   $ 
Horizontal=�6"   .+�5;      �� $ Vertical=�6   .+�5<      �� $��Nombre: ��6   �� 5=      $	��150��63   ��6   �� *Nombre+63   5>      �� 5?      $ Vertical=�6   .+�5@      �� $��Apellido 1: ��6   �� 5A      $	��150��66   ��6   �� *	Apellido1+66   5B      �� 5C      $ Vertical=�6   .+�5D      �� $��Apellido 2: ��6   �� 5E      $	��150��65   ��6   �� *	Apellido2+65   5F      5G      �� 5H   
   $��7��6   .+�5I      �� $
��Auto��6   $	��110��6%   $��0,10,0,0q�62   $��1,1,1,1q�6D   $��Stretch=�6^   $��Stretch=�6�   $��Stretch=�6�   $��Stretch=�6�   ���	#FFFFFFFF���	#FFCCCCCC.��5J      &   System.Windows.Controls.DataGrid  -   5          Name$ tablaCustomer��5J         AutoGenerateColumns .     
IsReadOnly .    SelectionMode$ Single=�5K      $��5q�62    �� AlternationCount$ 2��6=   $��Arial)�6R   $��12(�6e      GridLinesVisibility$
 None=�5L      $   HorizontalScrollBarVisibility$ Disabled=�66   "   VerticalScrollBarVisibility$ Visible=�6_   ���	#FFFFFFFF   CanUserAddRows .     CanUserDeleteRows .  ��. $��90��5M   �   $	��690��6�   ��5J   2   ��    Columns 5N      0  *System.Windows.Controls.DataGridTextColumn  5O      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 80* 6-     HeaderStyleMt   HeaderTemplate# [ 69      Header$ PersonID��6i   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6{   �� *
PersonID+  5P      $	 80* 6-   # [ 69   $ LastName��6i    6{   �� *
LastName+  5Q      $	 80* 6-   # [ 69   $ 	FirstName��6i    6|   �� *	FirstName+  5R      $	 80* 6-   # [ 69   $ DateHour��6i    6{   �� *
DateHour+ �� StringFormat$ {0:hh:mm tt}�� �� 	Converter# [    5S      $	 80* 6-   # [ 69   $ Fecha��6i    6x   �� *Fecha+5U      5V      5W      5X      5Y      