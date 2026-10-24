$fn=48;
translate([0,0,0])
{
difference()
{
    translate(v = [2.00, 2.00, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 2, r2 = 2, h = 22);
            translate(v = [42.00, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 22, $fn = 50);
            }
            translate(v = [0.00, 30.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 22, $fn = 50);
            }
            translate(v = [42.00, 30.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 22, $fn = 50);
            }
        }
    }
    translate(v = [0.00, 0.00, 13.00])
    {
        union()
        {
            union()
            {
                difference()
                {
                    translate(v = [2.00, 2.00, 0.00])
                    {
                        hull()
                        {
                            cylinder(center = false, r1 = 2, r2 = 2, h = 9);
                            translate(v = [42.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 2, r2 = 2, h = 9, $fn = 50);
                            }
                            translate(v = [0.00, 30.00, 0.00])
                            {
                                cylinder(center = false, r1 = 2, r2 = 2, h = 9, $fn = 50);
                            }
                            translate(v = [42.00, 30.00, 0.00])
                            {
                                cylinder(center = false, r1 = 2, r2 = 2, h = 9, $fn = 50);
                            }
                        }
                    }
                    translate(v = [1.75, 1.75, -0.01])
                    {
                        translate(v = [0.25, 0.25, 0.00])
                        {
                            hull()
                            {
                                cylinder(center = false, r1 = 0.25, r2 = 0.25, h = 7.4);
                                translate(v = [42.00, 0.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 0.25, r2 = 0.25, h = 7.4, $fn = 50);
                                }
                                translate(v = [0.00, 30.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 0.25, r2 = 0.25, h = 7.4, $fn = 50);
                                }
                                translate(v = [42.00, 30.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 0.25, r2 = 0.25, h = 7.4, $fn = 50);
                                }
                            }
                        }
                    }
                }
                translate(v = [1.75, 24.00, 0.00])
                {
                    rotate([90.00, 0.00, 0.00])
                    {
                        hull()
                        {
                            translate(v = [0.00, 2.78, 0.00])
                            {
                                cube(size = [0.05, 1.84, 8.00], center = false); 
                            }
                            translate(v = [0.55, 3.82, 0.00])
                            {
                                cube(size = [0.05, 0.20, 8.00], center = false); 
                            }
                        }
                    }
                }
                translate(v = [44.25, 16.00, 0.00])
                {
                    rotate([90.00, 0.00, 180.00])
                    {
                        hull()
                        {
                            translate(v = [0.00, 2.78, 0.00])
                            {
                                cube(size = [0.05, 1.84, 8.00], center = false); 
                            }
                            translate(v = [0.55, 3.82, 0.00])
                            {
                                cube(size = [0.05, 0.20, 8.00], center = false); 
                            }
                        }
                    }
                }
            }
            translate(v = [15.00, 0.00, 0.00])
            {
                rotate([90.00, 0.00, 90.00])
                {
                    translate(v = [-4.58, 0.00, 0.00])
                    {
                        cylinder(center = false, r1 = 6.083333333333333, r2 = 6.083333333333333, h = 16, $fn = 64);
                    }
                }
            }
        }
    }
    translate(v = [5.00, 5.00, 4.40])
    {
        cube(size = [36.00, 24.00, 16.00], center = false); 
    }
}

}
