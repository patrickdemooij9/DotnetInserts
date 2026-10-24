$fn=48;
translate([0,0,0])
{
difference()
{
    cube(size = [90.00, 70.00, 25.00], center = false); 
    translate(v = [3.00, 3.00, 2.00])
    {
        cube(size = [84.00, 64.00, 23.00], center = false); 
    }
    translate(v = [45.00, 0.60, 12.50])
    {
        rotate([90.00, 0.00, 0.00])
        {
            scale(v = [1.00, 1.00, 0.65])
            {
                linear_extrude(height = 1)
                {
                    text("FRONT", size = 8, halign = "center", valign = "center");
                }
            }
        }
    }
    translate(v = [45.00, 69.40, 12.50])
    {
        rotate([90.00, 0.00, 180.00])
        {
            scale(v = [1.00, 1.00, 0.65])
            {
                linear_extrude(height = 1)
                {
                    text("BACK", size = 8, halign = "center", valign = "center");
                }
            }
        }
    }
    translate(v = [0.60, 35.00, 12.50])
    {
        rotate([90.00, 0.00, -90.00])
        {
            scale(v = [1.00, 1.00, 0.65])
            {
                linear_extrude(height = 1)
                {
                    text("LEFT", size = 6, halign = "center", valign = "center");
                }
            }
        }
    }
    translate(v = [89.40, 35.00, 12.50])
    {
        rotate([90.00, 0.00, 90.00])
        {
            scale(v = [1.00, 1.00, 0.65])
            {
                linear_extrude(height = 1)
                {
                    text("RIGHT", size = 6, halign = "center", valign = "center");
                }
            }
        }
    }
}

}
translate([96,0,0])
{
difference()
{
    cube(size = [70.00, 70.00, 25.00], center = false); 
    translate(v = [3.00, 3.00, 2.00])
    {
        cube(size = [64.00, 64.00, 23.00], center = false); 
    }
    translate(v = [35.00, 1.20, 5.50])
    {
        rotate([90.00, 0.00, 0.00])
        {
            scale(v = [1.00, 1.00, 1.25])
            {
                linear_extrude(height = 1)
                {
                    text("LOW", size = 7, halign = "center", valign = "center");
                }
            }
        }
    }
    translate(v = [59.00, 0.60, 18.50])
    {
        rotate([90.00, 0.00, 0.00])
        {
            scale(v = [1.00, 1.00, 0.65])
            {
                linear_extrude(height = 1)
                {
                    text("R", size = 7, halign = "center", valign = "center");
                }
            }
        }
    }
}

}
translate([172,0,0])
{
difference()
{
    difference()
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
                        translate(v = [9.67, -3.00, -4.10])
                        {
                            cube(size = [48.67, 50.00, 24.10], center = false); 
                        }
                    }
                    translate(v = [5.67, -1.00, -4.00])
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
        translate(v = [36.50, 50.00, 3.40])
        {
            scale(v = [1.00, 1.00, 0.65])
            {
                linear_extrude(height = 1)
                {
                    text("Floor", size = 10, halign = "center", valign = "center");
                }
            }
        }
    }
    translate(v = [72.40, 50.00, 16.00])
    {
        rotate([90.00, 0.00, 90.00])
        {
            scale(v = [1.00, 1.00, 0.65])
            {
                linear_extrude(height = 1)
                {
                    text("SIDE", size = 8, halign = "center", valign = "center");
                }
            }
        }
    }
}

}
