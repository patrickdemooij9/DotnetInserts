/*Code Generated using OSCADSharp on 30/05/2025 09:12:49. 

For more information, please visit https://github.com/Exolun/OSCADSharp */

module Render1()
{
    difference()
    {
        cube(size = [88.00, 95.00, 45.00], center = false); 
        translate(v = [10.00, 0.50, 2.00])
        {
            union()
            {
                union()
                {
                    union()
                    {
                        union()
                        {
                            cube(size = [68.00, 94.00, 43.00], center = false); 
                            translate(v = [83.00, 22.00, -10.00])
                            {
                                rotate([0.00, 0.00, 90.00])
                                {
                                    cube(size = [50.00, 30.00, 53.00], center = false); 
                                }
                            }
                        }
                        translate(v = [15.00, 22.00, -10.00])
                        {
                            rotate([0.00, 0.00, 90.00])
                            {
                                cube(size = [50.00, 30.00, 53.00], center = false); 
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
                                            cube(size = [50.00, 4.00, 18.00], center = false); 
                                        }
                                    }
                                    translate(v = [-29.00, -2.00, 14.00])
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
                                        translate(v = [-29.00, -2.00, 14.00])
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
                translate(v = [0.00, 18.00, 0.00])
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
                                        cube(size = [50.00, 4.00, 18.00], center = false); 
                                    }
                                }
                                translate(v = [-29.00, -2.00, 14.00])
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
                                    translate(v = [-29.00, -2.00, 14.00])
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
        translate(v = [80.00, 2.50, 0.00])
        {
            cube(size = [8.00, 90.00, 45.00], center = false); 
        }
        translate(v = [0.00, 2.50, 0.00])
        {
            cube(size = [8.00, 90.00, 45.00], center = false); 
        }
    }
}

