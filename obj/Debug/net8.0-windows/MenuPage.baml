   M S B A M L   `   `   `  ����A  =JoinUp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:JoinUpJoinUp  5          JoinUp.MenuPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      &localclr-namespace:JoinUp     5        N� Title$  MenuPage��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��199* �6   �� 5      $
��201* �6   .+�5   
   �� -   5         ��Name$ createEvent��5      $��258,0,258,10q�6&   $��32��6<   $��Bottom=�6H   $��2��6{   .��6�   '%Создать мероприятие6�   �� 5   
   -   5         ��Name$ flagText��5      $��10,0,199,70q�6&   $��17��6;   $��Bottom=�6G    АЫААЫАЗЫАДЗЫАЫЗ6s   �� 5   
   -   5        $ 	nameUserr��5      $��10,0,157,46q�6'   $��23��6<   $��Bottom=�6H   ,*АЫААЫАЗЫАДgsgsgsgsgsgsЗЫАЫЗ6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ eventsDataGrid��5      $��10,0,10,100q�6+      AutoGenerateColumns$
 True��6@   $��2��6[   $	��300��6o   $��Bottom=�6|   �� 5   
   -   5        $ openVisitButt��5      $'��!Открыть посещение��6(   $
��Left=�6D   $��
201,0,0,56q�6_   $��1��6s   $	��150��6�   $��27��6�   $��Bottom=�6�   �� 5   
   -   5        $ sendBidButt��5      $��1��6<   $%��Отправить заявку��6L   $
��Left=�6g   $��
201,0,0,10q�6�   $��Bottom=�6�   $��28��6�   $	��150��6�   �� 5   
   -   5        $ 
updateButt��5      $��Обновить��6%   $
��Left=�6R   $��	70,0,0,12q�6m   $��Bottom=�6�   $��98��6�   �� 5   
   -   5        $ deleteEvent��5      $+��%Удалить мероприятие��6?   $
��Left=�6]   $��	27,0,0,56q�6x   $��Bottom=�6�   $��1��6�   $	��144��6�   $��27��6�   5      5      