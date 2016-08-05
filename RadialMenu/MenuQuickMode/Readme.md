##Quick mode RadialMenu##
This example demonstrates the expected behavior when the RadRadialMenu.EnableQuickMode is set to True. The default value of that static Boolean property is False. By design when the RadRadialMenu is being shown or closed its animation will be triggered. While that animation is being animated if a different animation is triggered it will not be executed and the initial animation will complete fully. When the EnableQuickMode is set to True that initial animation will be interrupted and the new one will be executed.

<keywords: EnableQuickMode, animation>