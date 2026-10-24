$fn=48;
translate([0,0,0])
{
difference()
{
    cube(size = [212.00, 80.00, 4.00], center = false); 
    translate(v = [11.00, 73.00, 4.00])
    {
        translate(v = [5.50, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.5, r2 = 5.5, h = 3, $fn = 100);
        }
    }
    translate(v = [28.00, 73.00, 4.00])
    {
        translate(v = [5.50, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.5, r2 = 5.5, h = 3, $fn = 100);
        }
    }
    translate(v = [11.00, 40.00, 4.00])
    {
        translate(v = [5.50, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.5, r2 = 5.5, h = 3, $fn = 100);
        }
    }
    translate(v = [28.00, 40.00, 4.00])
    {
        translate(v = [5.50, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.5, r2 = 5.5, h = 3, $fn = 100);
        }
    }
    translate(v = [87.00, 73.00, 4.00])
    {
        translate(v = [5.50, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.5, r2 = 5.5, h = 3, $fn = 100);
        }
    }
    translate(v = [130.00, 73.00, 4.00])
    {
        translate(v = [5.50, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.5, r2 = 5.5, h = 3, $fn = 100);
        }
    }
    translate(v = [87.00, 48.00, 4.00])
    {
        translate(v = [5.50, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.5, r2 = 5.5, h = 3, $fn = 100);
        }
    }
    translate(v = [130.00, 48.00, 4.00])
    {
        translate(v = [5.50, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.5, r2 = 5.5, h = 3, $fn = 100);
        }
    }
    translate(v = [167.00, 47.00, 0.00])
    {
        cube(size = [43.00, 31.00, 4.00], center = false); 
    }
    translate(v = [11.00, 6.00, 4.00])
    {
        translate(v = [5.25, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.25, r2 = 5.25, h = 5, $fn = 100);
        }
    }
    translate(v = [11.00, 33.00, 4.00])
    {
        translate(v = [5.25, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.25, r2 = 5.25, h = 5, $fn = 100);
        }
    }
    translate(v = [86.00, 4.00, 4.00])
    {
        translate(v = [5.50, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.5, r2 = 5.5, h = 3, $fn = 100);
        }
    }
    translate(v = [86.00, 34.00, 4.00])
    {
        translate(v = [5.50, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.5, r2 = 5.5, h = 3, $fn = 100);
        }
    }
    translate(v = [119.00, 4.00, 4.00])
    {
        translate(v = [5.50, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.5, r2 = 5.5, h = 3, $fn = 100);
        }
    }
    translate(v = [119.00, 34.00, 4.00])
    {
        translate(v = [5.50, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.5, r2 = 5.5, h = 3, $fn = 100);
        }
    }
    translate(v = [190.00, 4.00, 4.00])
    {
        translate(v = [5.50, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.5, r2 = 5.5, h = 3, $fn = 100);
        }
    }
    translate(v = [190.00, 31.00, 4.00])
    {
        translate(v = [5.50, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.5, r2 = 5.5, h = 3, $fn = 100);
        }
    }
    translate(v = [156.00, 4.00, 4.00])
    {
        translate(v = [5.50, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.5, r2 = 5.5, h = 3, $fn = 100);
        }
    }
    translate(v = [156.00, 31.00, 4.00])
    {
        translate(v = [5.50, 2.50, 0.00])
        {
            cylinder(center = false, r1 = 5.5, r2 = 5.5, h = 3, $fn = 100);
        }
    }
}

}
translate([0,80,0])
{
difference()
{
    cube(size = [70.60, 96.00, 39.00], center = false); 
    translate(v = [1.30, 1.00, 1.50])
    {
        union()
        {
            union()
            {
                cube(size = [68.00, 94.00, 37.50], center = false); 
                translate(v = [10.47, -1.00, -1.60])
                {
                    cube(size = [47.07, 48.00, 39.10], center = false); 
                }
            }
            translate(v = [6.47, 1.00, -1.50])
            {
                translate(v = [27.53, 0.00, 23.53])
                {
                    union()
                    {
                        hull()
                        {
                            rotate([90.00, 0.00, 0.00])
                            {
                                difference()
                                {
                                    cylinder(center = true, r1 = 23.53333333333333, r2 = 23.53333333333333, h = 4);
                                    translate(v = [-47.07, 0.00, -2.00])
                                    {
                                        cube(size = [94.13, 47.07, 4.00], center = false); 
                                    }
                                }
                            }
                            translate(v = [-23.53, -2.00, 0.00])
                            {
                                cube(size = [47.07, 4.00, 15.47], center = false); 
                            }
                        }
                        translate(v = [-27.53, -2.00, 11.47])
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
                        mirror([47.07, 0.00, 0.00])
                        {
                            translate(v = [-27.53, -2.00, 11.47])
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
translate([70.6,80,0])
{
difference()
{
    cube(size = [70.60, 96.00, 19.50], center = false); 
    translate(v = [1.30, 1.00, 1.50])
    {
        union()
        {
            union()
            {
                cube(size = [68.00, 94.00, 18.00], center = false); 
                translate(v = [10.47, -1.00, -1.60])
                {
                    cube(size = [47.07, 48.00, 19.60], center = false); 
                }
            }
            translate(v = [6.47, 1.00, -1.50])
            {
                translate(v = [27.53, 0.00, 23.53])
                {
                    union()
                    {
                        hull()
                        {
                            rotate([90.00, 0.00, 0.00])
                            {
                                difference()
                                {
                                    cylinder(center = true, r1 = 23.53333333333333, r2 = 23.53333333333333, h = 4);
                                    translate(v = [-47.07, 0.00, -2.00])
                                    {
                                        cube(size = [94.13, 47.07, 4.00], center = false); 
                                    }
                                }
                            }
                            translate(v = [-23.53, -2.00, 0.00])
                            {
                                cube(size = [47.07, 4.00, -4.03], center = false); 
                            }
                        }
                        translate(v = [-27.53, -2.00, -8.03])
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
                        mirror([47.07, 0.00, 0.00])
                        {
                            translate(v = [-27.53, -2.00, -8.03])
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
translate([70.6,80,19.5])
{
difference()
{
    cube(size = [70.60, 96.00, 19.50], center = false); 
    translate(v = [1.30, 1.00, 1.50])
    {
        union()
        {
            union()
            {
                cube(size = [68.00, 94.00, 18.00], center = false); 
                translate(v = [10.47, -1.00, -1.60])
                {
                    cube(size = [47.07, 48.00, 19.60], center = false); 
                }
            }
            translate(v = [6.47, 1.00, -1.50])
            {
                translate(v = [27.53, 0.00, 23.53])
                {
                    union()
                    {
                        hull()
                        {
                            rotate([90.00, 0.00, 0.00])
                            {
                                difference()
                                {
                                    cylinder(center = true, r1 = 23.53333333333333, r2 = 23.53333333333333, h = 4);
                                    translate(v = [-47.07, 0.00, -2.00])
                                    {
                                        cube(size = [94.13, 47.07, 4.00], center = false); 
                                    }
                                }
                            }
                            translate(v = [-23.53, -2.00, 0.00])
                            {
                                cube(size = [47.07, 4.00, -4.03], center = false); 
                            }
                        }
                        translate(v = [-27.53, -2.00, -8.03])
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
                        mirror([47.07, 0.00, 0.00])
                        {
                            translate(v = [-27.53, -2.00, -8.03])
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
translate([141.33,80,0])
{
difference()
{
    cube(size = [70.60, 96.00, 19.50], center = false); 
    translate(v = [1.30, 1.00, 1.50])
    {
        union()
        {
            union()
            {
                cube(size = [68.00, 94.00, 18.00], center = false); 
                translate(v = [10.47, -1.00, -1.60])
                {
                    cube(size = [47.07, 48.00, 19.60], center = false); 
                }
            }
            translate(v = [6.47, 1.00, -1.50])
            {
                translate(v = [27.53, 0.00, 23.53])
                {
                    union()
                    {
                        hull()
                        {
                            rotate([90.00, 0.00, 0.00])
                            {
                                difference()
                                {
                                    cylinder(center = true, r1 = 23.53333333333333, r2 = 23.53333333333333, h = 4);
                                    translate(v = [-47.07, 0.00, -2.00])
                                    {
                                        cube(size = [94.13, 47.07, 4.00], center = false); 
                                    }
                                }
                            }
                            translate(v = [-23.53, -2.00, 0.00])
                            {
                                cube(size = [47.07, 4.00, -4.03], center = false); 
                            }
                        }
                        translate(v = [-27.53, -2.00, -8.03])
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
                        mirror([47.07, 0.00, 0.00])
                        {
                            translate(v = [-27.53, -2.00, -8.03])
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
translate([141.33,80,19.5])
{
difference()
{
    cube(size = [70.60, 96.00, 19.50], center = false); 
    translate(v = [1.30, 1.00, 1.50])
    {
        union()
        {
            union()
            {
                cube(size = [68.00, 94.00, 18.00], center = false); 
                translate(v = [10.47, -1.00, -1.60])
                {
                    cube(size = [47.07, 48.00, 19.60], center = false); 
                }
            }
            translate(v = [6.47, 1.00, -1.50])
            {
                translate(v = [27.53, 0.00, 23.53])
                {
                    union()
                    {
                        hull()
                        {
                            rotate([90.00, 0.00, 0.00])
                            {
                                difference()
                                {
                                    cylinder(center = true, r1 = 23.53333333333333, r2 = 23.53333333333333, h = 4);
                                    translate(v = [-47.07, 0.00, -2.00])
                                    {
                                        cube(size = [94.13, 47.07, 4.00], center = false); 
                                    }
                                }
                            }
                            translate(v = [-23.53, -2.00, 0.00])
                            {
                                cube(size = [47.07, 4.00, -4.03], center = false); 
                            }
                        }
                        translate(v = [-27.53, -2.00, -8.03])
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
                        mirror([47.07, 0.00, 0.00])
                        {
                            translate(v = [-27.53, -2.00, -8.03])
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
translate([0,176,0])
{
difference()
{
    cube(size = [212.00, 50.00, 19.50], center = false); 
    translate(v = [2.00, 1.00, 1.00])
    {
        union()
        {
            union()
            {
                translate(v = [0.00, 0.00, 4.88])
                {
                    cube(size = [33.00, 48.00, 14.63], center = false); 
                }
                translate(v = [16.50, 0.00, 4.88])
                {
                    scale(v = [16.50, 48.00, 4.88])
                    {
                        rotate([-90.00, 0.00, 0.00])
                        {
                            cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                        }
                    }
                }
            }
            translate(v = [35.00, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 4.88])
                    {
                        cube(size = [33.00, 48.00, 14.63], center = false); 
                    }
                    translate(v = [16.50, 0.00, 4.88])
                    {
                        scale(v = [16.50, 48.00, 4.88])
                        {
                            rotate([-90.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                            }
                        }
                    }
                }
            }
            translate(v = [70.00, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 4.88])
                    {
                        cube(size = [33.00, 48.00, 14.63], center = false); 
                    }
                    translate(v = [16.50, 0.00, 4.88])
                    {
                        scale(v = [16.50, 48.00, 4.88])
                        {
                            rotate([-90.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                            }
                        }
                    }
                }
            }
            translate(v = [105.00, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 4.88])
                    {
                        cube(size = [33.00, 48.00, 14.63], center = false); 
                    }
                    translate(v = [16.50, 0.00, 4.88])
                    {
                        scale(v = [16.50, 48.00, 4.88])
                        {
                            rotate([-90.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                            }
                        }
                    }
                }
            }
            translate(v = [140.00, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 4.88])
                    {
                        cube(size = [33.00, 48.00, 14.63], center = false); 
                    }
                    translate(v = [16.50, 0.00, 4.88])
                    {
                        scale(v = [16.50, 48.00, 4.88])
                        {
                            rotate([-90.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                            }
                        }
                    }
                }
            }
            translate(v = [175.00, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 4.88])
                    {
                        cube(size = [33.00, 48.00, 14.63], center = false); 
                    }
                    translate(v = [16.50, 0.00, 4.88])
                    {
                        scale(v = [16.50, 48.00, 4.88])
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
translate([0,226,0])
{
difference()
{
    cube(size = [212.00, 71.00, 28.00], center = false); 
    translate(v = [1.00, 1.00, 1.00])
    {
        union()
        {
            union()
            {
                translate(v = [0.00, 0.00, 7.00])
                {
                    cube(size = [33.33, 69.00, 21.00], center = false); 
                }
                translate(v = [16.67, 0.00, 7.00])
                {
                    scale(v = [16.67, 69.00, 7.00])
                    {
                        rotate([-90.00, 0.00, 0.00])
                        {
                            cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                        }
                    }
                }
            }
            translate(v = [35.33, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 7.00])
                    {
                        cube(size = [33.33, 69.00, 21.00], center = false); 
                    }
                    translate(v = [16.67, 0.00, 7.00])
                    {
                        scale(v = [16.67, 69.00, 7.00])
                        {
                            rotate([-90.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                            }
                        }
                    }
                }
            }
            translate(v = [70.67, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 7.00])
                    {
                        cube(size = [33.33, 69.00, 21.00], center = false); 
                    }
                    translate(v = [16.67, 0.00, 7.00])
                    {
                        scale(v = [16.67, 69.00, 7.00])
                        {
                            rotate([-90.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                            }
                        }
                    }
                }
            }
            translate(v = [106.00, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 7.00])
                    {
                        cube(size = [33.33, 69.00, 21.00], center = false); 
                    }
                    translate(v = [16.67, 0.00, 7.00])
                    {
                        scale(v = [16.67, 69.00, 7.00])
                        {
                            rotate([-90.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                            }
                        }
                    }
                }
            }
            translate(v = [141.33, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 7.00])
                    {
                        cube(size = [33.33, 69.00, 21.00], center = false); 
                    }
                    translate(v = [16.67, 0.00, 7.00])
                    {
                        scale(v = [16.67, 69.00, 7.00])
                        {
                            rotate([-90.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                            }
                        }
                    }
                }
            }
            translate(v = [176.67, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 7.00])
                    {
                        cube(size = [33.33, 69.00, 21.00], center = false); 
                    }
                    translate(v = [16.67, 0.00, 7.00])
                    {
                        scale(v = [16.67, 69.00, 7.00])
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
translate([0,176,19.5])
{
difference()
{
    cube(size = [212.00, 50.00, 19.50], center = false); 
    translate(v = [36.00, 1.00, 1.00])
    {
        union()
        {
            union()
            {
                translate(v = [0.00, 0.00, 4.88])
                {
                    cube(size = [33.40, 48.00, 14.63], center = false); 
                }
                translate(v = [16.70, 0.00, 4.88])
                {
                    scale(v = [16.70, 48.00, 4.88])
                    {
                        rotate([-90.00, 0.00, 0.00])
                        {
                            cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                        }
                    }
                }
            }
            translate(v = [35.40, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 4.88])
                    {
                        cube(size = [33.40, 48.00, 14.63], center = false); 
                    }
                    translate(v = [16.70, 0.00, 4.88])
                    {
                        scale(v = [16.70, 48.00, 4.88])
                        {
                            rotate([-90.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                            }
                        }
                    }
                }
            }
            translate(v = [70.80, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 4.88])
                    {
                        cube(size = [33.40, 48.00, 14.63], center = false); 
                    }
                    translate(v = [16.70, 0.00, 4.88])
                    {
                        scale(v = [16.70, 48.00, 4.88])
                        {
                            rotate([-90.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                            }
                        }
                    }
                }
            }
            translate(v = [106.20, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 4.88])
                    {
                        cube(size = [33.40, 48.00, 14.63], center = false); 
                    }
                    translate(v = [16.70, 0.00, 4.88])
                    {
                        scale(v = [16.70, 48.00, 4.88])
                        {
                            rotate([-90.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                            }
                        }
                    }
                }
            }
            translate(v = [141.60, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 4.88])
                    {
                        cube(size = [33.40, 48.00, 14.63], center = false); 
                    }
                    translate(v = [16.70, 0.00, 4.88])
                    {
                        scale(v = [16.70, 48.00, 4.88])
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
    translate(v = [1.00, 32.00, 2.50])
    {
        cube(size = [33.00, 17.00, 17.00], center = false); 
    }
    translate(v = [1.00, 1.00, 2.50])
    {
        union()
        {
            translate(v = [0.00, 0.00, 4.25])
            {
                cube(size = [33.00, 29.00, 12.75], center = false); 
            }
            translate(v = [16.50, 0.00, 4.25])
            {
                scale(v = [16.50, 29.00, 4.25])
                {
                    rotate([-90.00, 0.00, 0.00])
                    {
                        cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                    }
                }
            }
        }
    }
    translate(v = [7.50, 25.00, 11.50])
    {
        cube(size = [20.00, 25.00, 8.00], center = false); 
    }
}

}
translate([0,226,28])
{
difference()
{
    cube(size = [212.00, 71.00, 11.00], center = false); 
    translate(v = [1.00, 11.25, 0.50])
    {
        cube(size = [47.50, 48.50, 8.50], center = false); 
    }
    translate(v = [50.00, 16.25, 0.50])
    {
        cube(size = [37.00, 38.50, 8.50], center = false); 
    }
    translate(v = [123.50, 1.00, 1.00])
    {
        union()
        {
            union()
            {
                translate(v = [0.00, 0.00, 2.75])
                {
                    cube(size = [42.75, 69.00, 8.25], center = false); 
                }
                translate(v = [21.38, 0.00, 2.75])
                {
                    scale(v = [21.38, 69.00, 2.75])
                    {
                        rotate([-90.00, 0.00, 0.00])
                        {
                            cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                        }
                    }
                }
            }
            translate(v = [44.75, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 2.75])
                    {
                        cube(size = [42.75, 69.00, 8.25], center = false); 
                    }
                    translate(v = [21.38, 0.00, 2.75])
                    {
                        scale(v = [21.38, 69.00, 2.75])
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
    translate(v = [1.00, 8.50, 9.00])
    {
        cube(size = [86.00, 54.00, 2.00], center = false); 
    }
    translate(v = [89.00, 1.00, 1.00])
    {
        cube(size = [32.00, 69.00, 10.00], center = false); 
    }
}

}
