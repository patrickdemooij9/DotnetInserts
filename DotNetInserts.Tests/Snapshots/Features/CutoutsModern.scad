$fn=48;
translate([0,0,0])
{
difference()
{
    cube(size = [90.00, 90.00, 30.00], center = false); 
    translate(v = [3.00, 3.00, 4.00])
    {
        union()
        {
            union()
            {
                union()
                {
                    union()
                    {
                        cube(size = [84.00, 84.00, 26.00], center = false); 
                        translate(v = [31.00, -3.00, -14.00])
                        {
                            translate(v = [0.00, 0.00, 0.00])
                            {
                                cube(size = [22.00, 22.00, 40.00], center = false); 
                            }
                        }
                    }
                    translate(v = [87.00, 29.50, -12.00])
                    {
                        rotate([0.00, 0.00, 90.00])
                        {
                            translate(v = [0.00, 0.00, 0.00])
                            {
                                hull()
                                {
                                    translate(v = [5.00, 20.00, 0.00])
                                    {
                                        cylinder(center = false, r1 = 5, r2 = 5, h = 38, $fn = 50);
                                    }
                                    translate(v = [20.00, 20.00, 0.00])
                                    {
                                        cylinder(center = false, r1 = 5, r2 = 5, h = 38, $fn = 50);
                                    }
                                    translate(v = [0.00, 0.00, 0.00])
                                    {
                                        cube(size = [25.00, 1.00, 38.00], center = false); 
                                    }
                                }
                            }
                        }
                    }
                }
                translate(v = [22.00, 72.00, 6.00])
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
            translate(v = [17.00, 32.00, 11.00])
            {
                rotate([0.00, 0.00, 90.00])
                {
                    translate(v = [0.00, 0.00, 0.00])
                    {
                        cube(size = [20.00, 20.00, 15.00], center = false); 
                    }
                }
            }
        }
    }
}

}
