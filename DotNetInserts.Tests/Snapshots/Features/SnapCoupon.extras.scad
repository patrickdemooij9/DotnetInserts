$fn=48;
translate([0,0,0])
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
        translate(v = [1.60, 1.60, -0.01])
        {
            translate(v = [0.40, 0.40, 0.00])
            {
                hull()
                {
                    cylinder(center = false, r1 = 0.3999999999999999, r2 = 0.3999999999999999, h = 7.4);
                    translate(v = [42.00, 0.00, 0.00])
                    {
                        cylinder(center = false, r1 = 0.3999999999999999, r2 = 0.3999999999999999, h = 7.4, $fn = 50);
                    }
                    translate(v = [0.00, 30.00, 0.00])
                    {
                        cylinder(center = false, r1 = 0.3999999999999999, r2 = 0.3999999999999999, h = 7.4, $fn = 50);
                    }
                    translate(v = [42.00, 30.00, 0.00])
                    {
                        cylinder(center = false, r1 = 0.3999999999999999, r2 = 0.3999999999999999, h = 7.4, $fn = 50);
                    }
                }
            }
        }
        translate(v = [1.60, 24.60, 0.00])
        {
            rotate([90.00, 0.00, 0.00])
            {
                translate(v = [-1.65, 6.20, 0.00])
                {
                    cube(size = [1.70, 0.60, 14.60], center = false); 
                }
            }
        }
        translate(v = [1.60, 24.60, 0.00])
        {
            rotate([90.00, 0.00, 0.00])
            {
                translate(v = [-1.65, 0.60, 0.00])
                {
                    cube(size = [1.70, 0.60, 14.60], center = false); 
                }
            }
        }
        translate(v = [1.60, 24.60, 0.00])
        {
            rotate([90.00, 0.00, 0.00])
            {
                translate(v = [-1.65, 0.60, 0.00])
                {
                    cube(size = [1.70, 6.20, 0.60], center = false); 
                }
            }
        }
        translate(v = [44.40, 10.00, 0.00])
        {
            rotate([90.00, 0.00, 180.00])
            {
                translate(v = [-1.65, 6.20, 0.00])
                {
                    cube(size = [1.70, 0.60, 14.60], center = false); 
                }
            }
        }
        translate(v = [44.40, 10.00, 0.00])
        {
            rotate([90.00, 0.00, 180.00])
            {
                translate(v = [-1.65, 0.60, 0.00])
                {
                    cube(size = [1.70, 0.60, 14.60], center = false); 
                }
            }
        }
        translate(v = [44.40, 24.00, 0.00])
        {
            rotate([90.00, 0.00, 180.00])
            {
                translate(v = [-1.65, 0.60, 0.00])
                {
                    cube(size = [1.70, 6.20, 0.60], center = false); 
                }
            }
        }
    }
    translate(v = [1.60, 23.00, 0.00])
    {
        rotate([90.00, 0.00, 0.00])
        {
            hull()
            {
                translate(v = [0.00, 2.92, 0.00])
                {
                    cube(size = [0.05, 1.57, 6.00], center = false); 
                }
                translate(v = [0.45, 3.78, 0.00])
                {
                    cube(size = [0.05, 0.20, 6.00], center = false); 
                }
            }
        }
    }
    translate(v = [44.40, 17.00, 0.00])
    {
        rotate([90.00, 0.00, 180.00])
        {
            hull()
            {
                translate(v = [0.00, 2.92, 0.00])
                {
                    cube(size = [0.05, 1.57, 6.00], center = false); 
                }
                translate(v = [0.45, 3.78, 0.00])
                {
                    cube(size = [0.05, 0.20, 6.00], center = false); 
                }
            }
        }
    }
}

}
