$fn=48;
translate([0,0,0])
{
difference()
{
    translate(v = [2.00, 2.00, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 2, r2 = 2, h = 32);
            translate(v = [94.00, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 32, $fn = 50);
            }
            translate(v = [0.00, 70.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 32, $fn = 50);
            }
            translate(v = [94.00, 70.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 32, $fn = 50);
            }
        }
    }
    translate(v = [2.00, 2.00, 2.00])
    {
        union()
        {
            union()
            {
                cube(size = [94.00, 70.00, 30.00], center = false); 
                translate(v = [96.00, 20.00, -10.00])
                {
                    rotate([0.00, 0.00, 90.00])
                    {
                        translate(v = [0.00, 0.00, 0.00])
                        {
                            hull()
                            {
                                translate(v = [15.00, 15.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 15, r2 = 15, h = 40, $fn = 50);
                                }
                                translate(v = [15.00, 15.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 15, r2 = 15, h = 40, $fn = 50);
                                }
                                translate(v = [0.00, 0.00, 0.00])
                                {
                                    cube(size = [30.00, 1.00, 40.00], center = false); 
                                }
                            }
                        }
                    }
                }
            }
            translate(v = [81.00, 15.00, 10.00])
            {
                rotate([0.00, 0.00, 90.00])
                {
                    translate(v = [0.00, 0.00, 0.00])
                    {
                        translate(v = [20.00, 0.00, 15.00])
                        {
                            union()
                            {
                                hull()
                                {
                                    rotate([90.00, 0.00, 0.00])
                                    {
                                        difference()
                                        {
                                            cylinder(center = true, r1 = 15, r2 = 15, h = 30);
                                            translate(v = [-30.00, 0.00, -15.00])
                                            {
                                                cube(size = [60.00, 30.00, 30.00], center = false); 
                                            }
                                        }
                                    }
                                    translate(v = [-15.00, -15.00, 0.00])
                                    {
                                        cube(size = [30.00, 30.00, 5.00], center = false); 
                                    }
                                }
                                translate(v = [-20.00, -15.00, 0.00])
                                {
                                    difference()
                                    {
                                        cube(size = [5.00, 30.00, 5.00], center = false); 
                                        translate(v = [0.00, 30.00, 0.00])
                                        {
                                            rotate([90.00, 0.00, 0.00])
                                            {
                                                cylinder(center = false, r1 = 5, r2 = 5, h = 30, $fn = 100);
                                            }
                                        }
                                    }
                                }
                                mirror([30.00, 0.00, 0.00])
                                {
                                    translate(v = [-20.00, -15.00, 0.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [5.00, 30.00, 5.00], center = false); 
                                            translate(v = [0.00, 30.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 5, r2 = 5, h = 30, $fn = 100);
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
}

}
translate([98,0,0])
{
difference()
{
    translate(v = [2.00, 2.00, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 2, r2 = 2, h = 32);
            translate(v = [68.00, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 32, $fn = 50);
            }
            translate(v = [0.00, 116.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 32, $fn = 50);
            }
            translate(v = [68.00, 116.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 32, $fn = 50);
            }
        }
    }
    translate(v = [2.00, 12.50, 2.00])
    {
        union()
        {
            union()
            {
                union()
                {
                    union()
                    {
                        cube(size = [68.00, 95.00, 30.00], center = false); 
                        translate(v = [14.00, 67.50, -10.00])
                        {
                            translate(v = [0.00, 0.00, 0.00])
                            {
                                hull()
                                {
                                    translate(v = [5.00, 5.00, 0.00])
                                    {
                                        cylinder(center = false, r1 = 5, r2 = 5, h = 40, $fn = 50);
                                    }
                                    translate(v = [35.00, 5.00, 0.00])
                                    {
                                        cylinder(center = false, r1 = 5, r2 = 5, h = 40, $fn = 50);
                                    }
                                    translate(v = [0.00, 39.00, 0.00])
                                    {
                                        cube(size = [40.00, 1.00, 40.00], center = false); 
                                    }
                                }
                            }
                        }
                    }
                    translate(v = [28.00, 32.50, -10.00])
                    {
                        rotate([0.00, 0.00, 90.00])
                        {
                            translate(v = [-23.00, 0.00, 0.00])
                            {
                                hull()
                                {
                                    translate(v = [15.00, 15.00, 0.00])
                                    {
                                        cylinder(center = false, r1 = 15, r2 = 15, h = 40, $fn = 50);
                                    }
                                    translate(v = [15.00, 15.00, 0.00])
                                    {
                                        cylinder(center = false, r1 = 15, r2 = 15, h = 40, $fn = 50);
                                    }
                                    translate(v = [0.00, 29.00, 0.00])
                                    {
                                        cube(size = [30.00, 1.00, 40.00], center = false); 
                                    }
                                }
                            }
                        }
                    }
                }
                translate(v = [-1.00, 60.00, 10.00])
                {
                    translate(v = [0.00, 0.00, 0.00])
                    {
                        translate(v = [35.00, 0.00, 25.00])
                        {
                            union()
                            {
                                hull()
                                {
                                    rotate([90.00, 0.00, 0.00])
                                    {
                                        difference()
                                        {
                                            cylinder(center = true, r1 = 25, r2 = 25, h = 95);
                                            translate(v = [-50.00, 0.00, -47.50])
                                            {
                                                cube(size = [100.00, 50.00, 95.00], center = false); 
                                            }
                                        }
                                    }
                                    translate(v = [-25.00, -47.50, 0.00])
                                    {
                                        cube(size = [50.00, 95.00, -5.00], center = false); 
                                    }
                                }
                                translate(v = [-35.00, -47.50, -15.00])
                                {
                                    difference()
                                    {
                                        cube(size = [10.00, 95.00, 10.00], center = false); 
                                        translate(v = [0.00, 95.00, 0.00])
                                        {
                                            rotate([90.00, 0.00, 0.00])
                                            {
                                                cylinder(center = false, r1 = 10, r2 = 10, h = 95, $fn = 100);
                                            }
                                        }
                                    }
                                }
                                mirror([50.00, 0.00, 0.00])
                                {
                                    translate(v = [-35.00, -47.50, -15.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [10.00, 95.00, 10.00], center = false); 
                                            translate(v = [0.00, 95.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 10, r2 = 10, h = 95, $fn = 100);
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
            translate(v = [13.00, 27.50, 10.00])
            {
                rotate([0.00, 0.00, 90.00])
                {
                    translate(v = [-23.00, 0.00, 0.00])
                    {
                        translate(v = [20.00, 0.00, 15.00])
                        {
                            union()
                            {
                                hull()
                                {
                                    rotate([90.00, 0.00, 0.00])
                                    {
                                        difference()
                                        {
                                            cylinder(center = true, r1 = 15, r2 = 15, h = 30);
                                            translate(v = [-30.00, 0.00, -15.00])
                                            {
                                                cube(size = [60.00, 30.00, 30.00], center = false); 
                                            }
                                        }
                                    }
                                    translate(v = [-15.00, -15.00, 0.00])
                                    {
                                        cube(size = [30.00, 30.00, 5.00], center = false); 
                                    }
                                }
                                translate(v = [-20.00, -15.00, 0.00])
                                {
                                    difference()
                                    {
                                        cube(size = [5.00, 30.00, 5.00], center = false); 
                                        translate(v = [0.00, 30.00, 0.00])
                                        {
                                            rotate([90.00, 0.00, 0.00])
                                            {
                                                cylinder(center = false, r1 = 5, r2 = 5, h = 30, $fn = 100);
                                            }
                                        }
                                    }
                                }
                                mirror([30.00, 0.00, 0.00])
                                {
                                    translate(v = [-20.00, -15.00, 0.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [5.00, 30.00, 5.00], center = false); 
                                            translate(v = [0.00, 30.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 5, r2 = 5, h = 30, $fn = 100);
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
}

}
translate([0,74,0])
{
difference()
{
    translate(v = [2.00, 2.00, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 2, r2 = 2, h = 32);
            translate(v = [94.00, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 32, $fn = 50);
            }
            translate(v = [0.00, 42.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 32, $fn = 50);
            }
            translate(v = [94.00, 42.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 32, $fn = 50);
            }
        }
    }
    translate(v = [2.50, 3.50, 18.00])
    {
        union()
        {
            cube(size = [33.00, 39.00, 14.00], center = false); 
            translate(v = [95.50, 9.50, -5.00])
            {
                rotate([0.00, 0.00, 90.00])
                {
                    translate(v = [0.00, 0.00, 0.00])
                    {
                        hull()
                        {
                            translate(v = [5.00, 15.00, 0.00])
                            {
                                cylinder(center = false, r1 = 5, r2 = 5, h = 19, $fn = 50);
                            }
                            translate(v = [15.00, 15.00, 0.00])
                            {
                                cylinder(center = false, r1 = 5, r2 = 5, h = 19, $fn = 50);
                            }
                            translate(v = [0.00, 0.00, 0.00])
                            {
                                cube(size = [20.00, 1.00, 19.00], center = false); 
                            }
                        }
                    }
                }
            }
        }
    }
    translate(v = [36.50, 3.50, 18.00])
    {
        cube(size = [33.00, 39.00, 14.00], center = false); 
    }
    translate(v = [70.00, 2.00, 18.00])
    {
        union()
        {
            cube(size = [24.00, 20.00, 14.00], center = false); 
            translate(v = [-50.00, 0.00, -5.00])
            {
                rotate([0.00, 0.00, 90.00])
                {
                    translate(v = [11.00, 0.00, 0.00])
                    {
                        hull()
                        {
                            translate(v = [5.00, 5.00, 0.00])
                            {
                                cylinder(center = false, r1 = 5, r2 = 5, h = 19, $fn = 50);
                            }
                            translate(v = [15.00, 5.00, 0.00])
                            {
                                cylinder(center = false, r1 = 5, r2 = 5, h = 19, $fn = 50);
                            }
                            translate(v = [0.00, 19.00, 0.00])
                            {
                                cube(size = [20.00, 1.00, 19.00], center = false); 
                            }
                        }
                    }
                }
            }
        }
    }
    translate(v = [70.00, 24.00, 18.00])
    {
        cube(size = [24.00, 20.00, 14.00], center = false); 
    }
}

}
