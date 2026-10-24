$fn=48;
translate([0,0,0])
{
difference()
{
    cube(size = [250.00, 142.00, 98.00], center = false); 
    translate(v = [0.00, 0.00, 49.00])
    {
        difference()
        {
            cube(size = [250.00, 142.00, 49.00], center = false); 
            translate(v = [1.50, 1.50, 0.00])
            {
                cube(size = [247.00, 139.00, 46.00], center = false); 
            }
        }
    }
    translate(v = [3.00, 2.00, 2.00])
    {
        union()
        {
            union()
            {
                cube(size = [244.00, 68.00, 93.00], center = false); 
                translate(v = [237.00, 6.00, 53.00])
                {
                    rotate([0.00, 0.00, 90.00])
                    {
                        translate(v = [0.00, 0.00, 0.00])
                        {
                            translate(v = [28.00, 0.00, 20.00])
                            {
                                union()
                                {
                                    hull()
                                    {
                                        rotate([90.00, 0.00, 0.00])
                                        {
                                            difference()
                                            {
                                                cylinder(center = true, r1 = 20, r2 = 20, h = 20);
                                                translate(v = [-40.00, 0.00, -10.00])
                                                {
                                                    cube(size = [80.00, 40.00, 20.00], center = false); 
                                                }
                                            }
                                        }
                                        translate(v = [-20.00, -10.00, 0.00])
                                        {
                                            cube(size = [40.00, 20.00, 20.00], center = false); 
                                        }
                                    }
                                    translate(v = [-28.00, -10.00, 12.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [8.00, 20.00, 8.00], center = false); 
                                            translate(v = [0.00, 20.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 8, r2 = 8, h = 20, $fn = 100);
                                                }
                                            }
                                        }
                                    }
                                    mirror([40.00, 0.00, 0.00])
                                    {
                                        translate(v = [-28.00, -10.00, 12.00])
                                        {
                                            difference()
                                            {
                                                cube(size = [8.00, 20.00, 8.00], center = false); 
                                                translate(v = [0.00, 20.00, 0.00])
                                                {
                                                    rotate([90.00, 0.00, 0.00])
                                                    {
                                                        cylinder(center = false, r1 = 8, r2 = 8, h = 20, $fn = 100);
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
            translate(v = [7.00, 6.00, 53.00])
            {
                rotate([0.00, 0.00, 90.00])
                {
                    translate(v = [0.00, 0.00, 0.00])
                    {
                        translate(v = [28.00, 0.00, 20.00])
                        {
                            union()
                            {
                                hull()
                                {
                                    rotate([90.00, 0.00, 0.00])
                                    {
                                        difference()
                                        {
                                            cylinder(center = true, r1 = 20, r2 = 20, h = 20);
                                            translate(v = [-40.00, 0.00, -10.00])
                                            {
                                                cube(size = [80.00, 40.00, 20.00], center = false); 
                                            }
                                        }
                                    }
                                    translate(v = [-20.00, -10.00, 0.00])
                                    {
                                        cube(size = [40.00, 20.00, 20.00], center = false); 
                                    }
                                }
                                translate(v = [-28.00, -10.00, 12.00])
                                {
                                    difference()
                                    {
                                        cube(size = [8.00, 20.00, 8.00], center = false); 
                                        translate(v = [0.00, 20.00, 0.00])
                                        {
                                            rotate([90.00, 0.00, 0.00])
                                            {
                                                cylinder(center = false, r1 = 8, r2 = 8, h = 20, $fn = 100);
                                            }
                                        }
                                    }
                                }
                                mirror([40.00, 0.00, 0.00])
                                {
                                    translate(v = [-28.00, -10.00, 12.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [8.00, 20.00, 8.00], center = false); 
                                            translate(v = [0.00, 20.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 8, r2 = 8, h = 20, $fn = 100);
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
    translate(v = [3.00, 72.00, 2.00])
    {
        union()
        {
            union()
            {
                cube(size = [244.00, 68.00, 93.00], center = false); 
                translate(v = [237.00, 6.00, 53.00])
                {
                    rotate([0.00, 0.00, 90.00])
                    {
                        translate(v = [0.00, 0.00, 0.00])
                        {
                            translate(v = [28.00, 0.00, 20.00])
                            {
                                union()
                                {
                                    hull()
                                    {
                                        rotate([90.00, 0.00, 0.00])
                                        {
                                            difference()
                                            {
                                                cylinder(center = true, r1 = 20, r2 = 20, h = 20);
                                                translate(v = [-40.00, 0.00, -10.00])
                                                {
                                                    cube(size = [80.00, 40.00, 20.00], center = false); 
                                                }
                                            }
                                        }
                                        translate(v = [-20.00, -10.00, 0.00])
                                        {
                                            cube(size = [40.00, 20.00, 20.00], center = false); 
                                        }
                                    }
                                    translate(v = [-28.00, -10.00, 12.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [8.00, 20.00, 8.00], center = false); 
                                            translate(v = [0.00, 20.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 8, r2 = 8, h = 20, $fn = 100);
                                                }
                                            }
                                        }
                                    }
                                    mirror([40.00, 0.00, 0.00])
                                    {
                                        translate(v = [-28.00, -10.00, 12.00])
                                        {
                                            difference()
                                            {
                                                cube(size = [8.00, 20.00, 8.00], center = false); 
                                                translate(v = [0.00, 20.00, 0.00])
                                                {
                                                    rotate([90.00, 0.00, 0.00])
                                                    {
                                                        cylinder(center = false, r1 = 8, r2 = 8, h = 20, $fn = 100);
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
            translate(v = [7.00, 6.00, 53.00])
            {
                rotate([0.00, 0.00, 90.00])
                {
                    translate(v = [0.00, 0.00, 0.00])
                    {
                        translate(v = [28.00, 0.00, 20.00])
                        {
                            union()
                            {
                                hull()
                                {
                                    rotate([90.00, 0.00, 0.00])
                                    {
                                        difference()
                                        {
                                            cylinder(center = true, r1 = 20, r2 = 20, h = 20);
                                            translate(v = [-40.00, 0.00, -10.00])
                                            {
                                                cube(size = [80.00, 40.00, 20.00], center = false); 
                                            }
                                        }
                                    }
                                    translate(v = [-20.00, -10.00, 0.00])
                                    {
                                        cube(size = [40.00, 20.00, 20.00], center = false); 
                                    }
                                }
                                translate(v = [-28.00, -10.00, 12.00])
                                {
                                    difference()
                                    {
                                        cube(size = [8.00, 20.00, 8.00], center = false); 
                                        translate(v = [0.00, 20.00, 0.00])
                                        {
                                            rotate([90.00, 0.00, 0.00])
                                            {
                                                cylinder(center = false, r1 = 8, r2 = 8, h = 20, $fn = 100);
                                            }
                                        }
                                    }
                                }
                                mirror([40.00, 0.00, 0.00])
                                {
                                    translate(v = [-28.00, -10.00, 12.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [8.00, 20.00, 8.00], center = false); 
                                            translate(v = [0.00, 20.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 8, r2 = 8, h = 20, $fn = 100);
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
translate([2,2,0])
{
difference()
{
    cube(size = [30.00, 67.00, 92.00], center = false); 
    translate(v = [1.00, 1.00, 1.00])
    {
        union()
        {
            union()
            {
                cube(size = [10.00, 65.00, 91.00], center = false); 
                translate(v = [9.00, 12.50, 71.00])
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
                                                cylinder(center = true, r1 = 15, r2 = 15, h = 40);
                                                translate(v = [-30.00, 0.00, -20.00])
                                                {
                                                    cube(size = [60.00, 30.00, 40.00], center = false); 
                                                }
                                            }
                                        }
                                        translate(v = [-15.00, -20.00, 0.00])
                                        {
                                            cube(size = [30.00, 40.00, 5.00], center = false); 
                                        }
                                    }
                                    translate(v = [-20.00, -20.00, 0.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [5.00, 40.00, 5.00], center = false); 
                                            translate(v = [0.00, 40.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 5, r2 = 5, h = 40, $fn = 100);
                                                }
                                            }
                                        }
                                    }
                                    mirror([30.00, 0.00, 0.00])
                                    {
                                        translate(v = [-20.00, -20.00, 0.00])
                                        {
                                            difference()
                                            {
                                                cube(size = [5.00, 40.00, 5.00], center = false); 
                                                translate(v = [0.00, 40.00, 0.00])
                                                {
                                                    rotate([90.00, 0.00, 0.00])
                                                    {
                                                        cylinder(center = false, r1 = 5, r2 = 5, h = 40, $fn = 100);
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
            translate(v = [19.00, 12.50, 71.00])
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
                                            cylinder(center = true, r1 = 15, r2 = 15, h = 40);
                                            translate(v = [-30.00, 0.00, -20.00])
                                            {
                                                cube(size = [60.00, 30.00, 40.00], center = false); 
                                            }
                                        }
                                    }
                                    translate(v = [-15.00, -20.00, 0.00])
                                    {
                                        cube(size = [30.00, 40.00, 5.00], center = false); 
                                    }
                                }
                                translate(v = [-20.00, -20.00, 0.00])
                                {
                                    difference()
                                    {
                                        cube(size = [5.00, 40.00, 5.00], center = false); 
                                        translate(v = [0.00, 40.00, 0.00])
                                        {
                                            rotate([90.00, 0.00, 0.00])
                                            {
                                                cylinder(center = false, r1 = 5, r2 = 5, h = 40, $fn = 100);
                                            }
                                        }
                                    }
                                }
                                mirror([30.00, 0.00, 0.00])
                                {
                                    translate(v = [-20.00, -20.00, 0.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [5.00, 40.00, 5.00], center = false); 
                                            translate(v = [0.00, 40.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 5, r2 = 5, h = 40, $fn = 100);
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
    translate(v = [17.00, 1.00, 1.00])
    {
        union()
        {
            cube(size = [12.00, 65.00, 91.00], center = false); 
            translate(v = [-7.00, 12.50, 81.00])
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
                                            cylinder(center = true, r1 = 15, r2 = 15, h = 40);
                                            translate(v = [-30.00, 0.00, -20.00])
                                            {
                                                cube(size = [60.00, 30.00, 40.00], center = false); 
                                            }
                                        }
                                    }
                                    translate(v = [-15.00, -20.00, 0.00])
                                    {
                                        cube(size = [30.00, 40.00, -5.00], center = false); 
                                    }
                                }
                                translate(v = [-20.00, -20.00, -10.00])
                                {
                                    difference()
                                    {
                                        cube(size = [5.00, 40.00, 5.00], center = false); 
                                        translate(v = [0.00, 40.00, 0.00])
                                        {
                                            rotate([90.00, 0.00, 0.00])
                                            {
                                                cylinder(center = false, r1 = 5, r2 = 5, h = 40, $fn = 100);
                                            }
                                        }
                                    }
                                }
                                mirror([30.00, 0.00, 0.00])
                                {
                                    translate(v = [-20.00, -20.00, -10.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [5.00, 40.00, 5.00], center = false); 
                                            translate(v = [0.00, 40.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 5, r2 = 5, h = 40, $fn = 100);
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
    translate(v = [12.00, 1.00, 1.00])
    {
        cube(size = [4.00, 65.00, 91.00], center = false); 
    }
}

}
