$fn=48;
translate([0,0,0])
{
union()
{
    difference()
    {
        cube(size = [19.60, 82.70, 19.70], center = false); 
        difference()
        {
            translate(v = [19.60, 41.35, 9.85])
            {
                cube(size = [9.80, 72.70, 9.70], center = true); 
            }
            translate(v = [19.60, 6.50, 14.70])
            {
                rotate([0.00, 90.00, 0.00])
                {
                    cylinder(center = true, r1 = 12.5, r2 = 12.5, h = 5, $fn = 50);
                }
            }
        }
        translate(v = [1.00, 51.35, -10.30])
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
    translate(v = [0.00, 15.98, -4.40])
    {
        cube(size = [19.60, 9.40, 4.40], center = false); 
    }
    translate(v = [0.00, 57.33, -4.40])
    {
        cube(size = [19.60, 9.40, 4.40], center = false); 
    }
}

}
