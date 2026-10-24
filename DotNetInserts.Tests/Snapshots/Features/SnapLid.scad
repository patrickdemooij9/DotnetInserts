$fn=48;
translate([0,0,0])
{
difference()
{
    translate(v = [3.00, 3.00, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 3, r2 = 3, h = 45);
            translate(v = [89.00, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 3, r2 = 3, h = 45, $fn = 50);
            }
            translate(v = [0.00, 84.00, 0.00])
            {
                cylinder(center = false, r1 = 3, r2 = 3, h = 45, $fn = 50);
            }
            translate(v = [89.00, 84.00, 0.00])
            {
                cylinder(center = false, r1 = 3, r2 = 3, h = 45, $fn = 50);
            }
        }
    }
    translate(v = [0.00, 0.00, 33.00])
    {
        union()
        {
            union()
            {
                difference()
                {
                    translate(v = [3.00, 3.00, 0.00])
                    {
                        hull()
                        {
                            cylinder(center = false, r1 = 3, r2 = 3, h = 12);
                            translate(v = [89.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 3, r2 = 3, h = 12, $fn = 50);
                            }
                            translate(v = [0.00, 84.00, 0.00])
                            {
                                cylinder(center = false, r1 = 3, r2 = 3, h = 12, $fn = 50);
                            }
                            translate(v = [89.00, 84.00, 0.00])
                            {
                                cylinder(center = false, r1 = 3, r2 = 3, h = 12, $fn = 50);
                            }
                        }
                    }
                    translate(v = [1.75, 1.75, -0.01])
                    {
                        translate(v = [1.25, 1.25, 0.00])
                        {
                            hull()
                            {
                                cylinder(center = false, r1 = 1.25, r2 = 1.25, h = 10);
                                translate(v = [89.00, 0.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 1.25, r2 = 1.25, h = 10, $fn = 50);
                                }
                                translate(v = [0.00, 84.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 1.25, r2 = 1.25, h = 10, $fn = 50);
                                }
                                translate(v = [89.00, 84.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 1.25, r2 = 1.25, h = 10, $fn = 50);
                                }
                            }
                        }
                    }
                }
                translate(v = [1.75, 58.00, 0.00])
                {
                    rotate([90.00, 0.00, 0.00])
                    {
                        hull()
                        {
                            translate(v = [0.00, 4.08, 0.00])
                            {
                                cube(size = [0.05, 1.84, 14.00], center = false); 
                            }
                            translate(v = [0.55, 5.12, 0.00])
                            {
                                cube(size = [0.05, 0.20, 14.00], center = false); 
                            }
                        }
                    }
                }
                translate(v = [93.25, 44.00, 0.00])
                {
                    rotate([90.00, 0.00, 180.00])
                    {
                        hull()
                        {
                            translate(v = [0.00, 4.08, 0.00])
                            {
                                cube(size = [0.05, 1.84, 14.00], center = false); 
                            }
                            translate(v = [0.55, 5.12, 0.00])
                            {
                                cube(size = [0.05, 0.20, 14.00], center = false); 
                            }
                        }
                    }
                }
            }
            translate(v = [59.50, 90.00, 0.00])
            {
                rotate([90.00, 0.00, -90.00])
                {
                    translate(v = [-10.45, 0.00, 0.00])
                    {
                        cylinder(center = false, r1 = 12.05, r2 = 12.05, h = 24, $fn = 64);
                    }
                }
            }
            translate(v = [35.50, 0.00, 0.00])
            {
                rotate([90.00, 0.00, 90.00])
                {
                    translate(v = [-10.45, 0.00, 0.00])
                    {
                        cylinder(center = false, r1 = 12.05, r2 = 12.05, h = 24, $fn = 64);
                    }
                }
            }
        }
    }
    translate(v = [6.00, 6.00, 7.00])
    {
        cube(size = [83.00, 78.00, 36.00], center = false); 
    }
}

}
translate([103,0,0])
{
difference()
{
    cube(size = [95.00, 90.00, 45.00], center = false); 
    translate(v = [0.00, 0.00, 31.00])
    {
        union()
        {
            difference()
            {
                cube(size = [95.00, 90.00, 14.00], center = false); 
                translate(v = [2.20, 2.20, -0.01])
                {
                    cube(size = [90.60, 85.60, 11.60], center = false); 
                }
            }
            translate(v = [2.20, 60.00, 0.00])
            {
                rotate([90.00, 0.00, 0.00])
                {
                    hull()
                    {
                        translate(v = [0.00, 4.28, 0.00])
                        {
                            cube(size = [0.05, 3.04, 16.00], center = false); 
                        }
                        translate(v = [0.80, 6.10, 0.00])
                        {
                            cube(size = [0.05, 0.20, 16.00], center = false); 
                        }
                    }
                }
            }
            translate(v = [92.80, 44.00, 0.00])
            {
                rotate([90.00, 0.00, 180.00])
                {
                    hull()
                    {
                        translate(v = [0.00, 4.28, 0.00])
                        {
                            cube(size = [0.05, 3.04, 16.00], center = false); 
                        }
                        translate(v = [0.80, 6.10, 0.00])
                        {
                            cube(size = [0.05, 0.20, 16.00], center = false); 
                        }
                    }
                }
            }
            translate(v = [62.50, 87.80, 0.00])
            {
                rotate([90.00, 0.00, -90.00])
                {
                    hull()
                    {
                        translate(v = [0.00, 4.28, 0.00])
                        {
                            cube(size = [0.05, 3.04, 16.00], center = false); 
                        }
                        translate(v = [0.80, 6.10, 0.00])
                        {
                            cube(size = [0.05, 0.20, 16.00], center = false); 
                        }
                    }
                }
            }
            translate(v = [46.50, 2.20, 0.00])
            {
                rotate([90.00, 0.00, 90.00])
                {
                    hull()
                    {
                        translate(v = [0.00, 4.28, 0.00])
                        {
                            cube(size = [0.05, 3.04, 16.00], center = false); 
                        }
                        translate(v = [0.80, 6.10, 0.00])
                        {
                            cube(size = [0.05, 0.20, 16.00], center = false); 
                        }
                    }
                }
            }
        }
    }
    translate(v = [7.00, 7.00, 6.60])
    {
        cube(size = [81.00, 76.00, 36.00], center = false); 
    }
}

}
