   M S B A M L   `   `   `  ����A  =JoinUp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:JoinUpJoinUp  5   	       JoinUp.VisitEvents   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   &localclr-namespace:JoinUp     5   	     � Title$  VisitEvents��5   	   $	��450��6   $	��800��6*    � 	Resources 5          JoinUp.AttendanceConverter   AttendanceConverter��%   &	          5   
   6$   5      .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ 
EventTitle��5      $��20(�6(   $
��Bold"�66   $��0,0,0,10q�6H   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ participantsDataGrid��5      $��1��61      AutoGenerateColumns$ False��6>      SelectionMode$ Single=�6Z      CanUserAddRows$ False��6q      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Имя��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6K   1  +System.Windows.Controls.DataGridBoundColumn   Binding 62   �� *Name+6K     5      $	 Явка��6%   $	
 100 6�    63   �� *
Attendance+ �� 	Converter# [  6�   5      �� 5   
   -   5        $��2��5      $��Отметить явку��6   $��30��6S   $	��150��6_   $��Center=�6k   $��0,10,0,0q�6�   5      5      