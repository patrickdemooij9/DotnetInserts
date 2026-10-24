/*Code Generated using OSCADSharp on 30/05/2025 09:12:49. 

For more information, please visit https://github.com/Exolun/OSCADSharp */

module Render0()
{
    difference()
    {
        cube(size = [79.00, 95.00, 46.00], center = false); 
        translate(v = [1.50, 0.50, 0.80])
        {
            union()
            {
                union()
                {
                    cube(size = [68.00, 94.00, 45.20], center = false); 
                    translate(v = [93.00, 22.00, -10.00])
                    {
                        rotate([0.00, 0.00, 90.00])
                        {
                            cube(size = [50.00, 50.00, 55.20], center = false); 
                        }
                    }
                }
                translate(v = [70.00, 18.00, 0.00])
                {
                    rotate([0.00, 0.00, 90.00])
                    {
                        translate(v = [29.00, 0.00, 25.00])
                        {
                            union()
                            {
                                hull()
                                {
                                    rotate([90.00, 0.00, 0.00])
                                    {
                                        cylinder(center = true, r1 = 25, r2 = 25, h = 4);
                                    }
                                    translate(v = [-25.00, -2.00, 0.00])
                                    {
                                        cube(size = [50.00, 4.00, 20.20], center = false); 
                                    }
                                }
                                translate(v = [-29.00, -2.00, 16.20])
                                {
                                    difference()
                                    {
                                        cube(size = [4.00, 4.00, 4.00], center = false); 
                                        translate(v = [0.00, 4.00, 0.00])
                                        {
                                            rotate([90.00, 0.00, 0.00])
                                            {
                                                cylinder(center = false, r1 = 4, r2 = 4, h = 4, $fn = 100);
                                            }
                                        }
                                    }
                                }
                                mirror([50.00, 0.00, 0.00])
                                {
                                    translate(v = [-29.00, -2.00, 16.20])
                                    {
                                        difference()
                                        {
                                            cube(size = [4.00, 4.00, 4.00], center = false); 
                                            translate(v = [0.00, 4.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 4, r2 = 4, h = 4, $fn = 100);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        translate(v = [71.00, 2.50, 1.00])
        {
            cube(size = [8.00, 90.00, 45.00], center = false); 
        }
    }
}

