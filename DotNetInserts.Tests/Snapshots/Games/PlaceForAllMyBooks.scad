$fn=48;
translate([0,0,0])
{
difference()
{
    translate(v = [3.75, 3.75, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 36.95);
            translate(v = [65.50, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 36.95, $fn = 50);
            }
            translate(v = [0.00, 92.50, 0.00])
            {
                cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 36.95, $fn = 50);
            }
            translate(v = [65.50, 92.50, 0.00])
            {
                cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 36.95, $fn = 50);
            }
        }
    }
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
translate([73,0,0])
{
difference()
{
    translate(v = [3.75, 3.75, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 36.95);
            translate(v = [65.50, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 36.95, $fn = 50);
            }
            translate(v = [0.00, 92.50, 0.00])
            {
                cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 36.95, $fn = 50);
            }
            translate(v = [65.50, 92.50, 0.00])
            {
                cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 36.95, $fn = 50);
            }
        }
    }
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
translate([146,0,0])
{
difference()
{
    translate(v = [3.75, 3.75, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 14.2);
            translate(v = [65.50, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 14.2, $fn = 50);
            }
            translate(v = [0.00, 92.50, 0.00])
            {
                cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 14.2, $fn = 50);
            }
            translate(v = [65.50, 92.50, 0.00])
            {
                cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 14.2, $fn = 50);
            }
        }
    }
    translate(v = [2.50, 3.00, 1.20])
    {
        union()
        {
            union()
            {
                cube(size = [68.00, 94.00, 13.00], center = false); 
                translate(v = [9.67, -3.00, -1.30])
                {
                    cube(size = [48.67, 50.00, 14.30], center = false); 
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
                                cube(size = [48.67, 4.00, -10.13], center = false); 
                            }
                        }
                        translate(v = [-28.33, -2.00, -14.13])
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
                            translate(v = [-28.33, -2.00, -14.13])
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
translate([146,0,14.2])
{
difference()
{
    translate(v = [3.75, 3.75, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 14.2);
            translate(v = [65.50, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 14.2, $fn = 50);
            }
            translate(v = [0.00, 92.50, 0.00])
            {
                cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 14.2, $fn = 50);
            }
            translate(v = [65.50, 92.50, 0.00])
            {
                cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 14.2, $fn = 50);
            }
        }
    }
    translate(v = [2.50, 3.00, 1.20])
    {
        union()
        {
            union()
            {
                cube(size = [68.00, 94.00, 13.00], center = false); 
                translate(v = [9.67, -3.00, -1.30])
                {
                    cube(size = [48.67, 50.00, 14.30], center = false); 
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
                                cube(size = [48.67, 4.00, -10.13], center = false); 
                            }
                        }
                        translate(v = [-28.33, -2.00, -14.13])
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
                            translate(v = [-28.33, -2.00, -14.13])
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
translate([146,0,28.4])
{
difference()
{
    translate(v = [3.75, 3.75, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 8.550000000000004);
            translate(v = [65.50, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 8.550000000000004, $fn = 50);
            }
            translate(v = [0.00, 92.50, 0.00])
            {
                cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 8.550000000000004, $fn = 50);
            }
            translate(v = [65.50, 92.50, 0.00])
            {
                cylinder(center = false, r1 = 3.75, r2 = 3.75, h = 8.550000000000004, $fn = 50);
            }
        }
    }
    translate(v = [2.50, 3.00, 1.20])
    {
        union()
        {
            union()
            {
                cube(size = [68.00, 94.00, 7.35], center = false); 
                translate(v = [9.67, -3.00, -1.30])
                {
                    cube(size = [48.67, 50.00, 8.65], center = false); 
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
                                cube(size = [48.67, 4.00, -15.78], center = false); 
                            }
                        }
                        translate(v = [-28.33, -2.00, -19.78])
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
                            translate(v = [-28.33, -2.00, -19.78])
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
translate([0,100,0])
{
difference()
{
    translate(v = [3.50, 3.50, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 3.5, r2 = 3.5, h = 30);
            translate(v = [48.00, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 3.5, r2 = 3.5, h = 30, $fn = 50);
            }
            translate(v = [0.00, 27.00, 0.00])
            {
                cylinder(center = false, r1 = 3.5, r2 = 3.5, h = 30, $fn = 50);
            }
            translate(v = [48.00, 27.00, 0.00])
            {
                cylinder(center = false, r1 = 3.5, r2 = 3.5, h = 30, $fn = 50);
            }
        }
    }
    translate(v = [2.00, 2.00, 2.00])
    {
        union()
        {
            translate(v = [0.00, 0.00, 7.00])
            {
                cube(size = [35.00, 30.00, 21.00], center = false); 
            }
            translate(v = [17.50, 0.00, 7.00])
            {
                scale(v = [17.50, 30.00, 7.00])
                {
                    rotate([-90.00, 0.00, 0.00])
                    {
                        cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                    }
                }
            }
        }
    }
    translate(v = [42.00, 7.50, 9.00])
    {
        cube(size = [11.00, 19.00, 21.00], center = false); 
    }
    translate(v = [39.00, 0.00, 20.00])
    {
        cube(size = [16.00, 34.00, 10.00], center = false); 
    }
}

}
translate([0,134,0])
{
difference()
{
    translate(v = [3.50, 3.50, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 3.5, r2 = 3.5, h = 30);
            translate(v = [48.00, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 3.5, r2 = 3.5, h = 30, $fn = 50);
            }
            translate(v = [0.00, 27.00, 0.00])
            {
                cylinder(center = false, r1 = 3.5, r2 = 3.5, h = 30, $fn = 50);
            }
            translate(v = [48.00, 27.00, 0.00])
            {
                cylinder(center = false, r1 = 3.5, r2 = 3.5, h = 30, $fn = 50);
            }
        }
    }
    translate(v = [2.00, 2.00, 2.00])
    {
        union()
        {
            translate(v = [0.00, 0.00, 7.00])
            {
                cube(size = [51.00, 30.00, 21.00], center = false); 
            }
            translate(v = [25.50, 0.00, 7.00])
            {
                scale(v = [25.50, 30.00, 7.00])
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
translate([55,109,0])
{
difference()
{
    translate(v = [3.50, 3.50, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 3.5, r2 = 3.5, h = 30);
            translate(v = [157.00, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 3.5, r2 = 3.5, h = 30, $fn = 50);
            }
            translate(v = [0.00, 163.00, 0.00])
            {
                cylinder(center = false, r1 = 3.5, r2 = 3.5, h = 30, $fn = 50);
            }
            translate(v = [157.00, 163.00, 0.00])
            {
                cylinder(center = false, r1 = 3.5, r2 = 3.5, h = 30, $fn = 50);
            }
        }
    }
    translate(v = [2.00, 2.00, 2.00])
    {
        cube(size = [160.00, 166.00, 28.00], center = false); 
    }
    translate(v = [18.50, 40.00, 1.00])
    {
        cube(size = [127.00, 90.00, 1.00], center = false); 
    }
}

}
