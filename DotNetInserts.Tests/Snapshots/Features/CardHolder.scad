$fn=48;
translate([0,0,0])
{
difference()
{
    cube(size = [73.00, 100.00, 36.95], center = false); 
    translate(v = [2.50, 3.00, 1.20])
    {
        union()
        {
            union()
            {
                cube(size = [68.00, 94.00, 35.75], center = false); 
                translate(v = [9.67, -3.00, -1.30])
                {
                    cube(size = [48.67, 50.00, 37.05], center = false); 
                }
            }
            translate(v = [5.67, -1.00, -1.20])
            {
                translate(v = [28.33, 0.00, 24.33])
                {
                    union()
                    {
                        hull()
                        {
                            rotate([90.00, 0.00, 0.00])
                            {
                                difference()
                                {
                                    cylinder(center = true, r1 = 24.333333333333332, r2 = 24.333333333333332, h = 4);
                                    translate(v = [-48.67, 0.00, -2.00])
                                    {
                                        cube(size = [97.33, 48.67, 4.00], center = false); 
                                    }
                                }
                            }
                            translate(v = [-24.33, -2.00, 0.00])
                            {
                                cube(size = [48.67, 4.00, 12.62], center = false); 
                            }
                        }
                        translate(v = [-28.33, -2.00, 8.62])
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
                        mirror([48.67, 0.00, 0.00])
                        {
                            translate(v = [-28.33, -2.00, 8.62])
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

}
translate([77,0,0])
{
difference()
{
    difference()
    {
        union()
        {
            difference()
            {
                translate(v = [3.75, 3.75, 0.00])
                {
                    hull()
                    {
                        cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 24);
                        translate(v = [65.50, 0.00, 0.00])
                        {
                            cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 24, $fn = 50);
                        }
                        translate(v = [0.00, 92.50, 0.00])
                        {
                            cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 24, $fn = 50);
                        }
                        translate(v = [65.50, 92.50, 0.00])
                        {
                            cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 24, $fn = 50);
                        }
                    }
                }
                translate(v = [2.50, 3.00, 4.00])
                {
                    union()
                    {
                        union()
                        {
                            cube(size = [68.00, 94.00, 20.00], center = false); 
                            translate(v = [70.50, 22.67, -4.10])
                            {
                                rotate([0.00, 0.00, 90.00])
                                {
                                    cube(size = [48.67, 50.00, 24.10], center = false); 
                                }
                            }
                        }
                        translate(v = [68.50, 18.67, -4.00])
                        {
                            rotate([0.00, 0.00, 90.00])
                            {
                                translate(v = [28.33, 0.00, 24.33])
                                {
                                    union()
                                    {
                                        hull()
                                        {
                                            rotate([90.00, 0.00, 0.00])
                                            {
                                                difference()
                                                {
                                                    cylinder(center = true, r1 = 24.333333333333332, r2 = 24.333333333333332, h = 4);
                                                    translate(v = [-48.67, 0.00, -2.00])
                                                    {
                                                        cube(size = [97.33, 48.67, 4.00], center = false); 
                                                    }
                                                }
                                            }
                                            translate(v = [-24.33, -2.00, 0.00])
                                            {
                                                cube(size = [48.67, 4.00, -0.33], center = false); 
                                            }
                                        }
                                        translate(v = [-28.33, -2.00, -4.33])
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
                                        mirror([48.67, 0.00, 0.00])
                                        {
                                            translate(v = [-28.33, -2.00, -4.33])
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
            }
            translate(v = [31.50, 0.00, 24.00])
            {
                cube(size = [10.00, 0.80, 3.00], center = false); 
            }
            translate(v = [31.50, 98.80, 24.00])
            {
                cube(size = [10.00, 0.80, 3.00], center = false); 
            }
        }
        translate(v = [31.35, 0.00, 0.00])
        {
            cube(size = [10.30, 1.10, 3.30], center = false); 
        }
        translate(v = [31.35, 98.90, 0.00])
        {
            cube(size = [10.30, 1.10, 3.30], center = false); 
        }
    }
    translate(v = [36.50, 50.00, 3.40])
    {
        scale(v = [1.00, 1.00, 0.65])
        {
            linear_extrude(height = 1)
            {
                text("Events", size = 12, halign = "center", valign = "center");
            }
        }
    }
}

}
