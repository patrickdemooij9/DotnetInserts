/*Code Generated using OSCADSharp on 30/05/2025 09:12:49. 

For more information, please visit https://github.com/Exolun/OSCADSharp */

module Render2()
{
    difference()
    {
        translate(v = [3.00, 3.00, 0.00])
        {
            hull()
            {
                cylinder(center = false, r1 = 3, r2 = 3, h = 22.5);
                translate(v = [38.00, 0.00, 0.00])
                {
                    cylinder(center = false, r1 = 3, r2 = 3, h = 22.5, $fn = 50);
                }
                translate(v = [0.00, 184.00, 0.00])
                {
                    cylinder(center = false, r1 = 3, r2 = 3, h = 22.5, $fn = 50);
                }
                translate(v = [38.00, 184.00, 0.00])
                {
                    cylinder(center = false, r1 = 3, r2 = 3, h = 22.5, $fn = 50);
                }
            }
        }
        translate(v = [2.00, 2.00, 2.50])
        {
            union()
            {
                union()
                {
                    translate(v = [0.00, 0.00, 5.00])
                    {
                        cube(size = [40.00, 60.67, 15.00], center = false); 
                    }
                    translate(v = [20.00, 0.00, 5.00])
                    {
                        scale(v = [20.00, 60.67, 5.00])
                        {
                            rotate([-90.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                            }
                        }
                    }
                }
                translate(v = [0.00, 62.67, 0.00])
                {
                    union()
                    {
                        translate(v = [0.00, 0.00, 5.00])
                        {
                            cube(size = [40.00, 60.67, 15.00], center = false); 
                        }
                        translate(v = [20.00, 0.00, 5.00])
                        {
                            scale(v = [20.00, 60.67, 5.00])
                            {
                                rotate([-90.00, 0.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                                }
                            }
                        }
                    }
                }
                translate(v = [0.00, 125.33, 0.00])
                {
                    union()
                    {
                        translate(v = [0.00, 0.00, 5.00])
                        {
                            cube(size = [40.00, 60.67, 15.00], center = false); 
                        }
                        translate(v = [20.00, 0.00, 5.00])
                        {
                            scale(v = [20.00, 60.67, 5.00])
                            {
                                rotate([-90.00, 0.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

