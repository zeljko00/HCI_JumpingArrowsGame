   M S B A M L   `   `   `  ����L  HHCI_JumpingArrows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:HCI_JumpingArrows.user_controlsHCI_JumpingArrows.user_controls  5      1    +HCI_JumpingArrows.user_controls.GameControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?local-clr-namespace:HCI_JumpingArrows.user_controls     5      .��5	      � -   5          �Name$  ContentGrid��5	       � RowDefinitions 5
   
   �� 5      $	��410 �6   6+   �� 5      $��34 �6   6*   .+�5   
   �� -   5         ��Name$ Graphics��5      $��0��6#   $��2,2,2,0q�60   ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ 	#FF9DFF5A��6#    � Offset$ 0Q�65   � 5      $ 	#FF5AE691��6#   $ 1Q�65   5      5      � 5   
   -   5         �Name$ CustomProgressbar��5      $��1��61   $��30��6>   $��2,2,2,2q�6J   $��Bottom=�6[   $��LeftToRight=�6w    � Maximum$
 1000Q�6�   	 � Value	 6�   �� *ProgressBarValue+
 �� Mode$
 OneWay=���5      �� 5      $ 0.5,1&�6&   $ 0.5,0&�67    �� RelativeTransform 5      V� 5      .~�5       ��  �� CenterX$	 0.5Q�6-    �� CenterY$	 0.5Q�6;   6K   �� 5!       �� Angle$	 270Q�6.   6<   5"      .��5%      � $
 Lime��6#   $ 0Q�60   � 5&      $	 Red��6#    6/   �� *Offset+$
 OneWay=�5'      5*      �� 5+   
   $��Center=�6:   $��30��6W   $��0,0,0,2q�6c   $��Center=�6t   $��Bottom=�6�   $��70��6�   $��Medium"�6�   $��18(�6�   $��Center=�6�   $��1��6  ��6   �� *
Progress+$
 OneWay=�6  �� 5,   
   $
��Left=�6<   $��30��6W   $��	10,10,0,0q�6c   $	��Top=�6v   $
��auto��6�   $
��Bold"�6�   $��16(�6�   $
��Left=�6�   $��Center=�6�   ��6   �� *
ScoreValue+$
 OneWay=�6�   �� 5-   
   $��Right=�65   $��30��6Q   $��	0,10,10,0q�6]   $	��Top=�6p   $
��auto��6�   
���  ��$��16(�6�   $��Medium"�6�   ��6   �� *Msg+$
 OneWay=�6�   �� 5.   
   -   5         ��Name$ Label��5.      $��Center=�6G   $��50��6d   $��	0,100,0,0q�6p   $	��Top=�6�   $
��auto��6�   $��25(�6�   $��Medium"�6�   ��6   �� *EndMsg+$
 OneWay=�6�   �� 5/   
   $��Center=�6<   $��40��6Y   $��	0,150,0,0q�6e   $	��Top=�6x   $
��auto��6�   $��16(�6�   $��Medium"�6�   ��6   �� *
Statistics+$
 OneWay=�6�   51      52      