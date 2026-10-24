$fn=48;
translate([0,0,0])
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
        translate(v = [1.60, 1.60, -0.01])
        {
            translate(v = [1.40, 1.40, 0.00])
            {
                hull()
                {
                    cylinder(center = false, r1 = 1.4, r2 = 1.4, h = 10);
                    translate(v = [89.00, 0.00, 0.00])
                    {
                        cylinder(center = false, r1 = 1.4, r2 = 1.4, h = 10, $fn = 50);
                    }
                    translate(v = [0.00, 84.00, 0.00])
                    {
                        cylinder(center = false, r1 = 1.4, r2 = 1.4, h = 10, $fn = 50);
                    }
                    translate(v = [89.00, 84.00, 0.00])
                    {
                        cylinder(center = false, r1 = 1.4, r2 = 1.4, h = 10, $fn = 50);
                    }
                }
            }
        }
        translate(v = [1.60, 58.15, 0.00])
        {
            rotate([90.00, 0.00, 0.00])
            {
                translate(v = [-1.65, 8.00, 0.00])
                {
                    cube(size = [1.70, 0.15, 26.15], center = false); 
                }
            }
        }
        translate(v = [1.60, 58.15, 0.00])
        {
            rotate([90.00, 0.00, 0.00])
            {
                translate(v = [-1.65, 1.85, 0.00])
                {
                    cube(size = [1.70, 0.15, 26.15], center = false); 
                }
            }
        }
        translate(v = [1.60, 58.15, 0.00])
        {
            rotate([90.00, 0.00, 0.00])
            {
                translate(v = [-1.65, 1.85, 0.00])
                {
                    cube(size = [1.70, 6.30, 0.15], center = false); 
                }
            }
        }
        translate(v = [93.40, 32.00, 0.00])
        {
            rotate([90.00, 0.00, 180.00])
            {
                translate(v = [-1.65, 8.00, 0.00])
                {
                    cube(size = [1.70, 0.15, 26.15], center = false); 
                }
            }
        }
        translate(v = [93.40, 32.00, 0.00])
        {
            rotate([90.00, 0.00, 180.00])
            {
                translate(v = [-1.65, 1.85, 0.00])
                {
                    cube(size = [1.70, 0.15, 26.15], center = false); 
                }
            }
        }
        translate(v = [93.40, 58.00, 0.00])
        {
            rotate([90.00, 0.00, 180.00])
            {
                translate(v = [-1.65, 1.85, 0.00])
                {
                    cube(size = [1.70, 6.30, 0.15], center = false); 
                }
            }
        }
    }
    translate(v = [1.60, 57.00, 0.00])
    {
        rotate([90.00, 0.00, 0.00])
        {
            hull()
            {
                translate(v = [0.00, 4.22, 0.00])
                {
                    cube(size = [0.05, 1.57, 12.00], center = false); 
                }
                translate(v = [0.45, 5.08, 0.00])
                {
                    cube(size = [0.05, 0.20, 12.00], center = false); 
                }
            }
        }
    }
    translate(v = [93.40, 45.00, 0.00])
    {
        rotate([90.00, 0.00, 180.00])
        {
            hull()
            {
                translate(v = [0.00, 4.22, 0.00])
                {
                    cube(size = [0.05, 1.57, 12.00], center = false); 
                }
                translate(v = [0.45, 5.08, 0.00])
                {
                    cube(size = [0.05, 0.20, 12.00], center = false); 
                }
            }
        }
    }
}

}
translate([105,0,0])
{
union()
{
    difference()
    {
        cube(size = [95.00, 90.00, 14.00], center = false); 
        translate(v = [2.00, 2.00, -0.01])
        {
            cube(size = [91.00, 86.00, 11.60], center = false); 
        }
        translate(v = [2.00, 60.80, 0.00])
        {
            rotate([90.00, 0.00, 0.00])
            {
                translate(v = [-2.05, 9.30, 0.00])
                {
                    cube(size = [2.10, 0.80, 30.80], center = false); 
                }
            }
        }
        translate(v = [2.00, 60.80, 0.00])
        {
            rotate([90.00, 0.00, 0.00])
            {
                translate(v = [-2.05, 1.50, 0.00])
                {
                    cube(size = [2.10, 0.80, 30.80], center = false); 
                }
            }
        }
        translate(v = [2.00, 60.80, 0.00])
        {
            rotate([90.00, 0.00, 0.00])
            {
                translate(v = [-2.05, 1.50, 0.00])
                {
                    cube(size = [2.10, 8.60, 0.80], center = false); 
                }
            }
        }
        translate(v = [93.00, 30.00, 0.00])
        {
            rotate([90.00, 0.00, 180.00])
            {
                translate(v = [-2.05, 9.30, 0.00])
                {
                    cube(size = [2.10, 0.80, 30.80], center = false); 
                }
            }
        }
        translate(v = [93.00, 30.00, 0.00])
        {
            rotate([90.00, 0.00, 180.00])
            {
                translate(v = [-2.05, 1.50, 0.00])
                {
                    cube(size = [2.10, 0.80, 30.80], center = false); 
                }
            }
        }
        translate(v = [93.00, 60.00, 0.00])
        {
            rotate([90.00, 0.00, 180.00])
            {
                translate(v = [-2.05, 1.50, 0.00])
                {
                    cube(size = [2.10, 8.60, 0.80], center = false); 
                }
            }
        }
        translate(v = [63.30, 88.00, 0.00])
        {
            rotate([90.00, 0.00, -90.00])
            {
                translate(v = [-2.05, 9.30, 0.00])
                {
                    cube(size = [2.10, 0.80, 30.80], center = false); 
                }
            }
        }
        translate(v = [63.30, 88.00, 0.00])
        {
            rotate([90.00, 0.00, -90.00])
            {
                translate(v = [-2.05, 1.50, 0.00])
                {
                    cube(size = [2.10, 0.80, 30.80], center = false); 
                }
            }
        }
        translate(v = [63.30, 88.00, 0.00])
        {
            rotate([90.00, 0.00, -90.00])
            {
                translate(v = [-2.05, 1.50, 0.00])
                {
                    cube(size = [2.10, 8.60, 0.80], center = false); 
                }
            }
        }
        translate(v = [32.50, 2.00, 0.00])
        {
            rotate([90.00, 0.00, 90.00])
            {
                translate(v = [-2.05, 9.30, 0.00])
                {
                    cube(size = [2.10, 0.80, 30.80], center = false); 
                }
            }
        }
        translate(v = [32.50, 2.00, 0.00])
        {
            rotate([90.00, 0.00, 90.00])
            {
                translate(v = [-2.05, 1.50, 0.00])
                {
                    cube(size = [2.10, 0.80, 30.80], center = false); 
                }
            }
        }
        translate(v = [62.50, 2.00, 0.00])
        {
            rotate([90.00, 0.00, 90.00])
            {
                translate(v = [-2.05, 1.50, 0.00])
                {
                    cube(size = [2.10, 8.60, 0.80], center = false); 
                }
            }
        }
    }
    translate(v = [2.00, 59.00, 0.00])
    {
        rotate([90.00, 0.00, 0.00])
        {
            hull()
            {
                translate(v = [0.00, 4.53, 0.00])
                {
                    cube(size = [0.05, 2.54, 14.00], center = false); 
                }
                translate(v = [0.65, 6.03, 0.00])
                {
                    cube(size = [0.05, 0.20, 14.00], center = false); 
                }
            }
        }
    }
    translate(v = [93.00, 45.00, 0.00])
    {
        rotate([90.00, 0.00, 180.00])
        {
            hull()
            {
                translate(v = [0.00, 4.53, 0.00])
                {
                    cube(size = [0.05, 2.54, 14.00], center = false); 
                }
                translate(v = [0.65, 6.03, 0.00])
                {
                    cube(size = [0.05, 0.20, 14.00], center = false); 
                }
            }
        }
    }
    translate(v = [61.50, 88.00, 0.00])
    {
        rotate([90.00, 0.00, -90.00])
        {
            hull()
            {
                translate(v = [0.00, 4.53, 0.00])
                {
                    cube(size = [0.05, 2.54, 14.00], center = false); 
                }
                translate(v = [0.65, 6.03, 0.00])
                {
                    cube(size = [0.05, 0.20, 14.00], center = false); 
                }
            }
        }
    }
    translate(v = [47.50, 2.00, 0.00])
    {
        rotate([90.00, 0.00, 90.00])
        {
            hull()
            {
                translate(v = [0.00, 4.53, 0.00])
                {
                    cube(size = [0.05, 2.54, 14.00], center = false); 
                }
                translate(v = [0.65, 6.03, 0.00])
                {
                    cube(size = [0.05, 0.20, 14.00], center = false); 
                }
            }
        }
    }
}

}
