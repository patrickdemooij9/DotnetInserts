$fn=48;
translate([0,0,0])
{
difference()
{
    cube(size = [120.00, 90.00, 30.00], center = false); 
    translate(v = [5.00, 5.00, 4.00])
    {
        union()
        {
            union()
            {
                union()
                {
                    cube(size = [110.00, 80.00, 26.00], center = false); 
                    translate(v = [105.00, 12.00, -14.00])
                    {
                        rotate([0.00, 0.00, 90.00])
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
                translate(v = [5.00, 12.00, -14.00])
                {
                    rotate([0.00, 0.00, 90.00])
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
            translate(v = [6.00, -1.50, 0.00])
            {
                union()
                {
                    difference()
                    {
                        cube(size = [20.00, 83.00, 20.00], center = false); 
                        difference()
                        {
                            translate(v = [20.00, 41.50, 10.00])
                            {
                                cube(size = [10.00, 73.00, 10.00], center = true); 
                            }
                            translate(v = [20.00, 6.50, 15.00])
                            {
                                rotate([0.00, 90.00, 0.00])
                                {
                                    cylinder(center = true, r1 = 12.5, r2 = 12.5, h = 5, $fn = 50);
                                }
                            }
                        }
                        translate(v = [1.00, 51.50, -10.00])
                        {
                            translate(v = [6.00, 0.00, 1.00])
                            {
                                union()
                                {
                                    hull()
                                    {
                                        rotate([90.00, 0.00, 0.00])
                                        {
                                            difference()
                                            {
                                                cylinder(center = true, r1 = 1, r2 = 1, h = 20);
                                                translate(v = [-2.00, 0.00, -10.00])
                                                {
                                                    cube(size = [4.00, 2.00, 20.00], center = false); 
                                                }
                                            }
                                        }
                                        translate(v = [-1.00, -10.00, 0.00])
                                        {
                                            cube(size = [2.00, 20.00, 39.00], center = false); 
                                        }
                                    }
                                    translate(v = [-6.00, -10.00, 34.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [5.00, 20.00, 5.00], center = false); 
                                            translate(v = [0.00, 20.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 5, r2 = 5, h = 20, $fn = 100);
                                                }
                                            }
                                        }
                                    }
                                    mirror([2.00, 0.00, 0.00])
                                    {
                                        translate(v = [-6.00, -10.00, 34.00])
                                        {
                                            difference()
                                            {
                                                cube(size = [5.00, 20.00, 5.00], center = false); 
                                                translate(v = [0.00, 20.00, 0.00])
                                                {
                                                    rotate([90.00, 0.00, 0.00])
                                                    {
                                                        cylinder(center = false, r1 = 5, r2 = 5, h = 20, $fn = 100);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    translate(v = [0.00, 15.75, -5.00])
                    {
                        cube(size = [10.00, 10.00, 5.00], center = false); 
                    }
                    translate(v = [0.00, 57.25, -5.00])
                    {
                        cube(size = [10.00, 10.00, 5.00], center = false); 
                    }
                }
            }
            translate(v = [32.00, -1.50, 0.00])
            {
                union()
                {
                    difference()
                    {
                        cube(size = [20.00, 83.00, 20.00], center = false); 
                        difference()
                        {
                            translate(v = [20.00, 41.50, 10.00])
                            {
                                cube(size = [10.00, 73.00, 10.00], center = true); 
                            }
                            translate(v = [20.00, 6.50, 15.00])
                            {
                                rotate([0.00, 90.00, 0.00])
                                {
                                    cylinder(center = true, r1 = 12.5, r2 = 12.5, h = 5, $fn = 50);
                                }
                            }
                        }
                        translate(v = [1.00, 51.50, -10.00])
                        {
                            translate(v = [6.00, 0.00, 1.00])
                            {
                                union()
                                {
                                    hull()
                                    {
                                        rotate([90.00, 0.00, 0.00])
                                        {
                                            difference()
                                            {
                                                cylinder(center = true, r1 = 1, r2 = 1, h = 20);
                                                translate(v = [-2.00, 0.00, -10.00])
                                                {
                                                    cube(size = [4.00, 2.00, 20.00], center = false); 
                                                }
                                            }
                                        }
                                        translate(v = [-1.00, -10.00, 0.00])
                                        {
                                            cube(size = [2.00, 20.00, 39.00], center = false); 
                                        }
                                    }
                                    translate(v = [-6.00, -10.00, 34.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [5.00, 20.00, 5.00], center = false); 
                                            translate(v = [0.00, 20.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 5, r2 = 5, h = 20, $fn = 100);
                                                }
                                            }
                                        }
                                    }
                                    mirror([2.00, 0.00, 0.00])
                                    {
                                        translate(v = [-6.00, -10.00, 34.00])
                                        {
                                            difference()
                                            {
                                                cube(size = [5.00, 20.00, 5.00], center = false); 
                                                translate(v = [0.00, 20.00, 0.00])
                                                {
                                                    rotate([90.00, 0.00, 0.00])
                                                    {
                                                        cylinder(center = false, r1 = 5, r2 = 5, h = 20, $fn = 100);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    translate(v = [0.00, 15.75, -5.00])
                    {
                        cube(size = [10.00, 10.00, 5.00], center = false); 
                    }
                    translate(v = [0.00, 57.25, -5.00])
                    {
                        cube(size = [10.00, 10.00, 5.00], center = false); 
                    }
                }
            }
            translate(v = [58.00, -1.50, 0.00])
            {
                union()
                {
                    difference()
                    {
                        cube(size = [20.00, 83.00, 20.00], center = false); 
                        difference()
                        {
                            translate(v = [20.00, 41.50, 10.00])
                            {
                                cube(size = [10.00, 73.00, 10.00], center = true); 
                            }
                            translate(v = [20.00, 6.50, 15.00])
                            {
                                rotate([0.00, 90.00, 0.00])
                                {
                                    cylinder(center = true, r1 = 12.5, r2 = 12.5, h = 5, $fn = 50);
                                }
                            }
                        }
                        translate(v = [1.00, 51.50, -10.00])
                        {
                            translate(v = [6.00, 0.00, 1.00])
                            {
                                union()
                                {
                                    hull()
                                    {
                                        rotate([90.00, 0.00, 0.00])
                                        {
                                            difference()
                                            {
                                                cylinder(center = true, r1 = 1, r2 = 1, h = 20);
                                                translate(v = [-2.00, 0.00, -10.00])
                                                {
                                                    cube(size = [4.00, 2.00, 20.00], center = false); 
                                                }
                                            }
                                        }
                                        translate(v = [-1.00, -10.00, 0.00])
                                        {
                                            cube(size = [2.00, 20.00, 39.00], center = false); 
                                        }
                                    }
                                    translate(v = [-6.00, -10.00, 34.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [5.00, 20.00, 5.00], center = false); 
                                            translate(v = [0.00, 20.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 5, r2 = 5, h = 20, $fn = 100);
                                                }
                                            }
                                        }
                                    }
                                    mirror([2.00, 0.00, 0.00])
                                    {
                                        translate(v = [-6.00, -10.00, 34.00])
                                        {
                                            difference()
                                            {
                                                cube(size = [5.00, 20.00, 5.00], center = false); 
                                                translate(v = [0.00, 20.00, 0.00])
                                                {
                                                    rotate([90.00, 0.00, 0.00])
                                                    {
                                                        cylinder(center = false, r1 = 5, r2 = 5, h = 20, $fn = 100);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    translate(v = [0.00, 15.75, -5.00])
                    {
                        cube(size = [10.00, 10.00, 5.00], center = false); 
                    }
                    translate(v = [0.00, 57.25, -5.00])
                    {
                        cube(size = [10.00, 10.00, 5.00], center = false); 
                    }
                }
            }
            translate(v = [84.00, -1.50, 0.00])
            {
                union()
                {
                    difference()
                    {
                        cube(size = [20.00, 83.00, 20.00], center = false); 
                        difference()
                        {
                            translate(v = [20.00, 41.50, 10.00])
                            {
                                cube(size = [10.00, 73.00, 10.00], center = true); 
                            }
                            translate(v = [20.00, 6.50, 15.00])
                            {
                                rotate([0.00, 90.00, 0.00])
                                {
                                    cylinder(center = true, r1 = 12.5, r2 = 12.5, h = 5, $fn = 50);
                                }
                            }
                        }
                        translate(v = [1.00, 51.50, -10.00])
                        {
                            translate(v = [6.00, 0.00, 1.00])
                            {
                                union()
                                {
                                    hull()
                                    {
                                        rotate([90.00, 0.00, 0.00])
                                        {
                                            difference()
                                            {
                                                cylinder(center = true, r1 = 1, r2 = 1, h = 20);
                                                translate(v = [-2.00, 0.00, -10.00])
                                                {
                                                    cube(size = [4.00, 2.00, 20.00], center = false); 
                                                }
                                            }
                                        }
                                        translate(v = [-1.00, -10.00, 0.00])
                                        {
                                            cube(size = [2.00, 20.00, 39.00], center = false); 
                                        }
                                    }
                                    translate(v = [-6.00, -10.00, 34.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [5.00, 20.00, 5.00], center = false); 
                                            translate(v = [0.00, 20.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 5, r2 = 5, h = 20, $fn = 100);
                                                }
                                            }
                                        }
                                    }
                                    mirror([2.00, 0.00, 0.00])
                                    {
                                        translate(v = [-6.00, -10.00, 34.00])
                                        {
                                            difference()
                                            {
                                                cube(size = [5.00, 20.00, 5.00], center = false); 
                                                translate(v = [0.00, 20.00, 0.00])
                                                {
                                                    rotate([90.00, 0.00, 0.00])
                                                    {
                                                        cylinder(center = false, r1 = 5, r2 = 5, h = 20, $fn = 100);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    translate(v = [0.00, 15.75, -5.00])
                    {
                        cube(size = [10.00, 10.00, 5.00], center = false); 
                    }
                    translate(v = [0.00, 57.25, -5.00])
                    {
                        cube(size = [10.00, 10.00, 5.00], center = false); 
                    }
                }
            }
        }
    }
}

}
