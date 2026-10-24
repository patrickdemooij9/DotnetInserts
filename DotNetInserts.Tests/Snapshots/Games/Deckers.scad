$fn=48;
translate([0,0,0])
{
difference()
{
    translate(v = [4.00, 4.00, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 4, r2 = 4, h = 69.2);
            translate(v = [48.00, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 69.2, $fn = 50);
            }
            translate(v = [0.00, 90.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 69.2, $fn = 50);
            }
            translate(v = [48.00, 90.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 69.2, $fn = 50);
            }
        }
    }
    translate(v = [2.00, 2.00, 1.20])
    {
        union()
        {
            cube(size = [52.00, 94.00, 68.00], center = false); 
            translate(v = [-146.00, 17.00, 18.00])
            {
                rotate([0.00, 0.00, 90.00])
                {
                    translate(v = [0.00, 100.00, 0.00])
                    {
                        translate(v = [30.00, 0.00, 25.00])
                        {
                            union()
                            {
                                hull()
                                {
                                    rotate([90.00, 0.00, 0.00])
                                    {
                                        difference()
                                        {
                                            cylinder(center = true, r1 = 25, r2 = 25, h = 400);
                                            translate(v = [-50.00, 0.00, -200.00])
                                            {
                                                cube(size = [100.00, 50.00, 400.00], center = false); 
                                            }
                                        }
                                    }
                                    translate(v = [-25.00, -200.00, 0.00])
                                    {
                                        cube(size = [50.00, 400.00, 25.00], center = false); 
                                    }
                                }
                                translate(v = [-30.00, -200.00, 20.00])
                                {
                                    difference()
                                    {
                                        cube(size = [5.00, 400.00, 5.00], center = false); 
                                        translate(v = [0.00, 400.00, 0.00])
                                        {
                                            rotate([90.00, 0.00, 0.00])
                                            {
                                                cylinder(center = false, r1 = 5, r2 = 5, h = 400, $fn = 100);
                                            }
                                        }
                                    }
                                }
                                mirror([50.00, 0.00, 0.00])
                                {
                                    translate(v = [-30.00, -200.00, 20.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [5.00, 400.00, 5.00], center = false); 
                                            translate(v = [0.00, 400.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 5, r2 = 5, h = 400, $fn = 100);
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
translate([56,0,0])
{
difference()
{
    translate(v = [4.00, 4.00, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 4, r2 = 4, h = 69.2);
            translate(v = [56.65, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 69.2, $fn = 50);
            }
            translate(v = [0.00, 90.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 69.2, $fn = 50);
            }
            translate(v = [56.65, 90.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 69.2, $fn = 50);
            }
        }
    }
    translate(v = [2.00, 2.00, 1.20])
    {
        union()
        {
            union()
            {
                cube(size = [10.53, 94.00, 68.00], center = false); 
                translate(v = [-39.47, 17.00, 18.00])
                {
                    rotate([0.00, 0.00, 90.00])
                    {
                        translate(v = [0.00, 50.00, 0.00])
                        {
                            translate(v = [30.00, 0.00, 25.00])
                            {
                                union()
                                {
                                    hull()
                                    {
                                        rotate([90.00, 0.00, 0.00])
                                        {
                                            difference()
                                            {
                                                cylinder(center = true, r1 = 25, r2 = 25, h = 100);
                                                translate(v = [-50.00, 0.00, -50.00])
                                                {
                                                    cube(size = [100.00, 50.00, 100.00], center = false); 
                                                }
                                            }
                                        }
                                        translate(v = [-25.00, -50.00, 0.00])
                                        {
                                            cube(size = [50.00, 100.00, 25.00], center = false); 
                                        }
                                    }
                                    translate(v = [-30.00, -50.00, 20.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [5.00, 100.00, 5.00], center = false); 
                                            translate(v = [0.00, 100.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
                                                }
                                            }
                                        }
                                    }
                                    mirror([50.00, 0.00, 0.00])
                                    {
                                        translate(v = [-30.00, -50.00, 20.00])
                                        {
                                            difference()
                                            {
                                                cube(size = [5.00, 100.00, 5.00], center = false); 
                                                translate(v = [0.00, 100.00, 0.00])
                                                {
                                                    rotate([90.00, 0.00, 0.00])
                                                    {
                                                        cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
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
            translate(v = [12.53, 0.00, 0.00])
            {
                union()
                {
                    cube(size = [10.53, 94.00, 68.00], center = false); 
                    translate(v = [-39.47, 17.00, 18.00])
                    {
                        rotate([0.00, 0.00, 90.00])
                        {
                            translate(v = [0.00, 50.00, 0.00])
                            {
                                translate(v = [30.00, 0.00, 25.00])
                                {
                                    union()
                                    {
                                        hull()
                                        {
                                            rotate([90.00, 0.00, 0.00])
                                            {
                                                difference()
                                                {
                                                    cylinder(center = true, r1 = 25, r2 = 25, h = 100);
                                                    translate(v = [-50.00, 0.00, -50.00])
                                                    {
                                                        cube(size = [100.00, 50.00, 100.00], center = false); 
                                                    }
                                                }
                                            }
                                            translate(v = [-25.00, -50.00, 0.00])
                                            {
                                                cube(size = [50.00, 100.00, 25.00], center = false); 
                                            }
                                        }
                                        translate(v = [-30.00, -50.00, 20.00])
                                        {
                                            difference()
                                            {
                                                cube(size = [5.00, 100.00, 5.00], center = false); 
                                                translate(v = [0.00, 100.00, 0.00])
                                                {
                                                    rotate([90.00, 0.00, 0.00])
                                                    {
                                                        cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
                                                    }
                                                }
                                            }
                                        }
                                        mirror([50.00, 0.00, 0.00])
                                        {
                                            translate(v = [-30.00, -50.00, 20.00])
                                            {
                                                difference()
                                                {
                                                    cube(size = [5.00, 100.00, 5.00], center = false); 
                                                    translate(v = [0.00, 100.00, 0.00])
                                                    {
                                                        rotate([90.00, 0.00, 0.00])
                                                        {
                                                            cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
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
            translate(v = [25.06, 0.00, 0.00])
            {
                union()
                {
                    cube(size = [10.53, 94.00, 68.00], center = false); 
                    translate(v = [-39.47, 17.00, 18.00])
                    {
                        rotate([0.00, 0.00, 90.00])
                        {
                            translate(v = [0.00, 50.00, 0.00])
                            {
                                translate(v = [30.00, 0.00, 25.00])
                                {
                                    union()
                                    {
                                        hull()
                                        {
                                            rotate([90.00, 0.00, 0.00])
                                            {
                                                difference()
                                                {
                                                    cylinder(center = true, r1 = 25, r2 = 25, h = 100);
                                                    translate(v = [-50.00, 0.00, -50.00])
                                                    {
                                                        cube(size = [100.00, 50.00, 100.00], center = false); 
                                                    }
                                                }
                                            }
                                            translate(v = [-25.00, -50.00, 0.00])
                                            {
                                                cube(size = [50.00, 100.00, 25.00], center = false); 
                                            }
                                        }
                                        translate(v = [-30.00, -50.00, 20.00])
                                        {
                                            difference()
                                            {
                                                cube(size = [5.00, 100.00, 5.00], center = false); 
                                                translate(v = [0.00, 100.00, 0.00])
                                                {
                                                    rotate([90.00, 0.00, 0.00])
                                                    {
                                                        cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
                                                    }
                                                }
                                            }
                                        }
                                        mirror([50.00, 0.00, 0.00])
                                        {
                                            translate(v = [-30.00, -50.00, 20.00])
                                            {
                                                difference()
                                                {
                                                    cube(size = [5.00, 100.00, 5.00], center = false); 
                                                    translate(v = [0.00, 100.00, 0.00])
                                                    {
                                                        rotate([90.00, 0.00, 0.00])
                                                        {
                                                            cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
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
            translate(v = [37.59, 0.00, 0.00])
            {
                union()
                {
                    cube(size = [10.53, 94.00, 68.00], center = false); 
                    translate(v = [-39.47, 17.00, 18.00])
                    {
                        rotate([0.00, 0.00, 90.00])
                        {
                            translate(v = [0.00, 50.00, 0.00])
                            {
                                translate(v = [30.00, 0.00, 25.00])
                                {
                                    union()
                                    {
                                        hull()
                                        {
                                            rotate([90.00, 0.00, 0.00])
                                            {
                                                difference()
                                                {
                                                    cylinder(center = true, r1 = 25, r2 = 25, h = 100);
                                                    translate(v = [-50.00, 0.00, -50.00])
                                                    {
                                                        cube(size = [100.00, 50.00, 100.00], center = false); 
                                                    }
                                                }
                                            }
                                            translate(v = [-25.00, -50.00, 0.00])
                                            {
                                                cube(size = [50.00, 100.00, 25.00], center = false); 
                                            }
                                        }
                                        translate(v = [-30.00, -50.00, 20.00])
                                        {
                                            difference()
                                            {
                                                cube(size = [5.00, 100.00, 5.00], center = false); 
                                                translate(v = [0.00, 100.00, 0.00])
                                                {
                                                    rotate([90.00, 0.00, 0.00])
                                                    {
                                                        cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
                                                    }
                                                }
                                            }
                                        }
                                        mirror([50.00, 0.00, 0.00])
                                        {
                                            translate(v = [-30.00, -50.00, 20.00])
                                            {
                                                difference()
                                                {
                                                    cube(size = [5.00, 100.00, 5.00], center = false); 
                                                    translate(v = [0.00, 100.00, 0.00])
                                                    {
                                                        rotate([90.00, 0.00, 0.00])
                                                        {
                                                            cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
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
            translate(v = [50.12, 0.00, 0.00])
            {
                union()
                {
                    cube(size = [10.53, 94.00, 68.00], center = false); 
                    translate(v = [-39.47, 17.00, 18.00])
                    {
                        rotate([0.00, 0.00, 90.00])
                        {
                            translate(v = [0.00, 50.00, 0.00])
                            {
                                translate(v = [30.00, 0.00, 25.00])
                                {
                                    union()
                                    {
                                        hull()
                                        {
                                            rotate([90.00, 0.00, 0.00])
                                            {
                                                difference()
                                                {
                                                    cylinder(center = true, r1 = 25, r2 = 25, h = 100);
                                                    translate(v = [-50.00, 0.00, -50.00])
                                                    {
                                                        cube(size = [100.00, 50.00, 100.00], center = false); 
                                                    }
                                                }
                                            }
                                            translate(v = [-25.00, -50.00, 0.00])
                                            {
                                                cube(size = [50.00, 100.00, 25.00], center = false); 
                                            }
                                        }
                                        translate(v = [-30.00, -50.00, 20.00])
                                        {
                                            difference()
                                            {
                                                cube(size = [5.00, 100.00, 5.00], center = false); 
                                                translate(v = [0.00, 100.00, 0.00])
                                                {
                                                    rotate([90.00, 0.00, 0.00])
                                                    {
                                                        cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
                                                    }
                                                }
                                            }
                                        }
                                        mirror([50.00, 0.00, 0.00])
                                        {
                                            translate(v = [-30.00, -50.00, 20.00])
                                            {
                                                difference()
                                                {
                                                    cube(size = [5.00, 100.00, 5.00], center = false); 
                                                    translate(v = [0.00, 100.00, 0.00])
                                                    {
                                                        rotate([90.00, 0.00, 0.00])
                                                        {
                                                            cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
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
}

}
translate([120.65,0,0])
{
difference()
{
    translate(v = [4.00, 4.00, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 4, r2 = 4, h = 69.2);
            translate(v = [41.00, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 69.2, $fn = 50);
            }
            translate(v = [0.00, 90.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 69.2, $fn = 50);
            }
            translate(v = [41.00, 90.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 69.2, $fn = 50);
            }
        }
    }
    translate(v = [2.00, 2.00, 1.20])
    {
        cube(size = [5.20, 94.00, 68.00], center = false); 
    }
    translate(v = [9.20, 2.00, 1.20])
    {
        union()
        {
            union()
            {
                cube(size = [11.27, 94.00, 68.00], center = false); 
                translate(v = [-38.73, 17.00, 18.00])
                {
                    rotate([0.00, 0.00, 90.00])
                    {
                        translate(v = [0.00, 50.00, 0.00])
                        {
                            translate(v = [30.00, 0.00, 25.00])
                            {
                                union()
                                {
                                    hull()
                                    {
                                        rotate([90.00, 0.00, 0.00])
                                        {
                                            difference()
                                            {
                                                cylinder(center = true, r1 = 25, r2 = 25, h = 100);
                                                translate(v = [-50.00, 0.00, -50.00])
                                                {
                                                    cube(size = [100.00, 50.00, 100.00], center = false); 
                                                }
                                            }
                                        }
                                        translate(v = [-25.00, -50.00, 0.00])
                                        {
                                            cube(size = [50.00, 100.00, 25.00], center = false); 
                                        }
                                    }
                                    translate(v = [-30.00, -50.00, 20.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [5.00, 100.00, 5.00], center = false); 
                                            translate(v = [0.00, 100.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
                                                }
                                            }
                                        }
                                    }
                                    mirror([50.00, 0.00, 0.00])
                                    {
                                        translate(v = [-30.00, -50.00, 20.00])
                                        {
                                            difference()
                                            {
                                                cube(size = [5.00, 100.00, 5.00], center = false); 
                                                translate(v = [0.00, 100.00, 0.00])
                                                {
                                                    rotate([90.00, 0.00, 0.00])
                                                    {
                                                        cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
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
            translate(v = [13.27, 0.00, 0.00])
            {
                union()
                {
                    cube(size = [11.27, 94.00, 68.00], center = false); 
                    translate(v = [-38.73, 17.00, 18.00])
                    {
                        rotate([0.00, 0.00, 90.00])
                        {
                            translate(v = [0.00, 50.00, 0.00])
                            {
                                translate(v = [30.00, 0.00, 25.00])
                                {
                                    union()
                                    {
                                        hull()
                                        {
                                            rotate([90.00, 0.00, 0.00])
                                            {
                                                difference()
                                                {
                                                    cylinder(center = true, r1 = 25, r2 = 25, h = 100);
                                                    translate(v = [-50.00, 0.00, -50.00])
                                                    {
                                                        cube(size = [100.00, 50.00, 100.00], center = false); 
                                                    }
                                                }
                                            }
                                            translate(v = [-25.00, -50.00, 0.00])
                                            {
                                                cube(size = [50.00, 100.00, 25.00], center = false); 
                                            }
                                        }
                                        translate(v = [-30.00, -50.00, 20.00])
                                        {
                                            difference()
                                            {
                                                cube(size = [5.00, 100.00, 5.00], center = false); 
                                                translate(v = [0.00, 100.00, 0.00])
                                                {
                                                    rotate([90.00, 0.00, 0.00])
                                                    {
                                                        cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
                                                    }
                                                }
                                            }
                                        }
                                        mirror([50.00, 0.00, 0.00])
                                        {
                                            translate(v = [-30.00, -50.00, 20.00])
                                            {
                                                difference()
                                                {
                                                    cube(size = [5.00, 100.00, 5.00], center = false); 
                                                    translate(v = [0.00, 100.00, 0.00])
                                                    {
                                                        rotate([90.00, 0.00, 0.00])
                                                        {
                                                            cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
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
            translate(v = [26.53, 0.00, 0.00])
            {
                union()
                {
                    cube(size = [11.27, 94.00, 68.00], center = false); 
                    translate(v = [-38.73, 17.00, 18.00])
                    {
                        rotate([0.00, 0.00, 90.00])
                        {
                            translate(v = [0.00, 50.00, 0.00])
                            {
                                translate(v = [30.00, 0.00, 25.00])
                                {
                                    union()
                                    {
                                        hull()
                                        {
                                            rotate([90.00, 0.00, 0.00])
                                            {
                                                difference()
                                                {
                                                    cylinder(center = true, r1 = 25, r2 = 25, h = 100);
                                                    translate(v = [-50.00, 0.00, -50.00])
                                                    {
                                                        cube(size = [100.00, 50.00, 100.00], center = false); 
                                                    }
                                                }
                                            }
                                            translate(v = [-25.00, -50.00, 0.00])
                                            {
                                                cube(size = [50.00, 100.00, 25.00], center = false); 
                                            }
                                        }
                                        translate(v = [-30.00, -50.00, 20.00])
                                        {
                                            difference()
                                            {
                                                cube(size = [5.00, 100.00, 5.00], center = false); 
                                                translate(v = [0.00, 100.00, 0.00])
                                                {
                                                    rotate([90.00, 0.00, 0.00])
                                                    {
                                                        cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
                                                    }
                                                }
                                            }
                                        }
                                        mirror([50.00, 0.00, 0.00])
                                        {
                                            translate(v = [-30.00, -50.00, 20.00])
                                            {
                                                difference()
                                                {
                                                    cube(size = [5.00, 100.00, 5.00], center = false); 
                                                    translate(v = [0.00, 100.00, 0.00])
                                                    {
                                                        rotate([90.00, 0.00, 0.00])
                                                        {
                                                            cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
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
}

}
translate([169.65,0,0])
{
difference()
{
    translate(v = [4.00, 4.00, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 4, r2 = 4, h = 69.2);
            translate(v = [10.35, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 69.2, $fn = 50);
            }
            translate(v = [0.00, 90.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 69.2, $fn = 50);
            }
            translate(v = [10.35, 90.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 69.2, $fn = 50);
            }
        }
    }
    translate(v = [2.00, 2.00, 1.20])
    {
        union()
        {
            union()
            {
                cube(size = [6.18, 94.00, 68.00], center = false); 
                translate(v = [-43.83, 17.00, 18.00])
                {
                    rotate([0.00, 0.00, 90.00])
                    {
                        translate(v = [0.00, 50.00, 0.00])
                        {
                            translate(v = [30.00, 0.00, 25.00])
                            {
                                union()
                                {
                                    hull()
                                    {
                                        rotate([90.00, 0.00, 0.00])
                                        {
                                            difference()
                                            {
                                                cylinder(center = true, r1 = 25, r2 = 25, h = 100);
                                                translate(v = [-50.00, 0.00, -50.00])
                                                {
                                                    cube(size = [100.00, 50.00, 100.00], center = false); 
                                                }
                                            }
                                        }
                                        translate(v = [-25.00, -50.00, 0.00])
                                        {
                                            cube(size = [50.00, 100.00, 25.00], center = false); 
                                        }
                                    }
                                    translate(v = [-30.00, -50.00, 20.00])
                                    {
                                        difference()
                                        {
                                            cube(size = [5.00, 100.00, 5.00], center = false); 
                                            translate(v = [0.00, 100.00, 0.00])
                                            {
                                                rotate([90.00, 0.00, 0.00])
                                                {
                                                    cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
                                                }
                                            }
                                        }
                                    }
                                    mirror([50.00, 0.00, 0.00])
                                    {
                                        translate(v = [-30.00, -50.00, 20.00])
                                        {
                                            difference()
                                            {
                                                cube(size = [5.00, 100.00, 5.00], center = false); 
                                                translate(v = [0.00, 100.00, 0.00])
                                                {
                                                    rotate([90.00, 0.00, 0.00])
                                                    {
                                                        cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
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
            translate(v = [8.18, 0.00, 0.00])
            {
                union()
                {
                    cube(size = [6.18, 94.00, 68.00], center = false); 
                    translate(v = [-43.83, 17.00, 18.00])
                    {
                        rotate([0.00, 0.00, 90.00])
                        {
                            translate(v = [0.00, 50.00, 0.00])
                            {
                                translate(v = [30.00, 0.00, 25.00])
                                {
                                    union()
                                    {
                                        hull()
                                        {
                                            rotate([90.00, 0.00, 0.00])
                                            {
                                                difference()
                                                {
                                                    cylinder(center = true, r1 = 25, r2 = 25, h = 100);
                                                    translate(v = [-50.00, 0.00, -50.00])
                                                    {
                                                        cube(size = [100.00, 50.00, 100.00], center = false); 
                                                    }
                                                }
                                            }
                                            translate(v = [-25.00, -50.00, 0.00])
                                            {
                                                cube(size = [50.00, 100.00, 25.00], center = false); 
                                            }
                                        }
                                        translate(v = [-30.00, -50.00, 20.00])
                                        {
                                            difference()
                                            {
                                                cube(size = [5.00, 100.00, 5.00], center = false); 
                                                translate(v = [0.00, 100.00, 0.00])
                                                {
                                                    rotate([90.00, 0.00, 0.00])
                                                    {
                                                        cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
                                                    }
                                                }
                                            }
                                        }
                                        mirror([50.00, 0.00, 0.00])
                                        {
                                            translate(v = [-30.00, -50.00, 20.00])
                                            {
                                                difference()
                                                {
                                                    cube(size = [5.00, 100.00, 5.00], center = false); 
                                                    translate(v = [0.00, 100.00, 0.00])
                                                    {
                                                        rotate([90.00, 0.00, 0.00])
                                                        {
                                                            cylinder(center = false, r1 = 5, r2 = 5, h = 100, $fn = 100);
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
}

}
translate([0,-100,0])
{
difference()
{
    translate(v = [4.00, 4.00, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 4, r2 = 4, h = 22);
            translate(v = [180.00, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 22, $fn = 50);
            }
            translate(v = [0.00, 84.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 22, $fn = 50);
            }
            translate(v = [180.00, 84.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 22, $fn = 50);
            }
        }
    }
    translate(v = [65.50, 6.00, 9.00])
    {
        union()
        {
            union()
            {
                union()
                {
                    union()
                    {
                        cube(size = [27.00, 52.00, 13.00], center = false); 
                        translate(v = [4.00, -6.00, -10.00])
                        {
                            translate(v = [0.00, 4.00, 0.00])
                            {
                                cube(size = [19.00, 14.00, 23.00], center = false); 
                            }
                        }
                    }
                    translate(v = [4.00, 72.00, -10.00])
                    {
                        translate(v = [0.00, -4.00, 0.00])
                        {
                            cube(size = [19.00, 14.00, 23.00], center = false); 
                        }
                    }
                }
                translate(v = [122.50, 13.50, -3.00])
                {
                    rotate([0.00, 0.00, 90.00])
                    {
                        translate(v = [0.00, 0.00, 0.00])
                        {
                            hull()
                            {
                                translate(v = [10.00, 10.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                                }
                                translate(v = [15.00, 10.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                                }
                                translate(v = [0.00, 0.00, 0.00])
                                {
                                    cube(size = [25.00, 1.00, 16.00], center = false); 
                                }
                            }
                        }
                    }
                }
            }
            translate(v = [-45.50, 13.50, -3.00])
            {
                rotate([0.00, 0.00, 90.00])
                {
                    translate(v = [0.00, 0.00, 0.00])
                    {
                        hull()
                        {
                            translate(v = [10.00, 10.00, 0.00])
                            {
                                cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                            }
                            translate(v = [15.00, 10.00, 0.00])
                            {
                                cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                            }
                            translate(v = [0.00, 19.00, 0.00])
                            {
                                cube(size = [25.00, 1.00, 16.00], center = false); 
                            }
                        }
                    }
                }
            }
        }
    }
    translate(v = [81.50, 63.50, 19.50])
    {
        translate(v = [12.50, 12.50, 0.00])
        {
            cylinder(center = false, r1 = 12.5, r2 = 12.5, h = 2.5, $fn = 100);
        }
    }
    translate(v = [35.50, 6.00, 9.00])
    {
        union()
        {
            union()
            {
                union()
                {
                    union()
                    {
                        cube(size = [27.00, 52.00, 13.00], center = false); 
                        translate(v = [4.00, -6.00, -10.00])
                        {
                            translate(v = [0.00, 4.00, 0.00])
                            {
                                cube(size = [19.00, 14.00, 23.00], center = false); 
                            }
                        }
                    }
                    translate(v = [4.00, 72.00, -10.00])
                    {
                        translate(v = [0.00, -4.00, 0.00])
                        {
                            cube(size = [19.00, 14.00, 23.00], center = false); 
                        }
                    }
                }
                translate(v = [152.50, 13.50, -3.00])
                {
                    rotate([0.00, 0.00, 90.00])
                    {
                        translate(v = [0.00, 0.00, 0.00])
                        {
                            hull()
                            {
                                translate(v = [10.00, 10.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                                }
                                translate(v = [15.00, 10.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                                }
                                translate(v = [0.00, 0.00, 0.00])
                                {
                                    cube(size = [25.00, 1.00, 16.00], center = false); 
                                }
                            }
                        }
                    }
                }
            }
            translate(v = [-15.50, 13.50, -3.00])
            {
                rotate([0.00, 0.00, 90.00])
                {
                    translate(v = [0.00, 0.00, 0.00])
                    {
                        hull()
                        {
                            translate(v = [10.00, 10.00, 0.00])
                            {
                                cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                            }
                            translate(v = [15.00, 10.00, 0.00])
                            {
                                cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                            }
                            translate(v = [0.00, 19.00, 0.00])
                            {
                                cube(size = [25.00, 1.00, 16.00], center = false); 
                            }
                        }
                    }
                }
            }
        }
    }
    translate(v = [5.50, 6.00, 9.00])
    {
        union()
        {
            union()
            {
                union()
                {
                    union()
                    {
                        cube(size = [27.00, 52.00, 13.00], center = false); 
                        translate(v = [4.00, -6.00, -10.00])
                        {
                            translate(v = [0.00, 4.00, 0.00])
                            {
                                cube(size = [19.00, 14.00, 23.00], center = false); 
                            }
                        }
                    }
                    translate(v = [4.00, 72.00, -10.00])
                    {
                        translate(v = [0.00, -4.00, 0.00])
                        {
                            cube(size = [19.00, 14.00, 23.00], center = false); 
                        }
                    }
                }
                translate(v = [182.50, 13.50, -3.00])
                {
                    rotate([0.00, 0.00, 90.00])
                    {
                        translate(v = [0.00, 0.00, 0.00])
                        {
                            hull()
                            {
                                translate(v = [10.00, 10.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                                }
                                translate(v = [15.00, 10.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                                }
                                translate(v = [0.00, 0.00, 0.00])
                                {
                                    cube(size = [25.00, 1.00, 16.00], center = false); 
                                }
                            }
                        }
                    }
                }
            }
            translate(v = [14.50, 13.50, -3.00])
            {
                rotate([0.00, 0.00, 90.00])
                {
                    translate(v = [0.00, 0.00, 0.00])
                    {
                        hull()
                        {
                            translate(v = [10.00, 10.00, 0.00])
                            {
                                cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                            }
                            translate(v = [15.00, 10.00, 0.00])
                            {
                                cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                            }
                            translate(v = [0.00, 19.00, 0.00])
                            {
                                cube(size = [25.00, 1.00, 16.00], center = false); 
                            }
                        }
                    }
                }
            }
        }
    }
    translate(v = [95.50, 6.00, 9.00])
    {
        union()
        {
            union()
            {
                union()
                {
                    union()
                    {
                        cube(size = [27.00, 52.00, 13.00], center = false); 
                        translate(v = [4.00, -6.00, -10.00])
                        {
                            translate(v = [0.00, 4.00, 0.00])
                            {
                                cube(size = [19.00, 14.00, 23.00], center = false); 
                            }
                        }
                    }
                    translate(v = [4.00, 72.00, -10.00])
                    {
                        translate(v = [0.00, -4.00, 0.00])
                        {
                            cube(size = [19.00, 14.00, 23.00], center = false); 
                        }
                    }
                }
                translate(v = [92.50, 13.50, -3.00])
                {
                    rotate([0.00, 0.00, 90.00])
                    {
                        translate(v = [0.00, 0.00, 0.00])
                        {
                            hull()
                            {
                                translate(v = [10.00, 10.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                                }
                                translate(v = [15.00, 10.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                                }
                                translate(v = [0.00, 0.00, 0.00])
                                {
                                    cube(size = [25.00, 1.00, 16.00], center = false); 
                                }
                            }
                        }
                    }
                }
            }
            translate(v = [-75.50, 13.50, -3.00])
            {
                rotate([0.00, 0.00, 90.00])
                {
                    translate(v = [0.00, 0.00, 0.00])
                    {
                        hull()
                        {
                            translate(v = [10.00, 10.00, 0.00])
                            {
                                cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                            }
                            translate(v = [15.00, 10.00, 0.00])
                            {
                                cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                            }
                            translate(v = [0.00, 19.00, 0.00])
                            {
                                cube(size = [25.00, 1.00, 16.00], center = false); 
                            }
                        }
                    }
                }
            }
        }
    }
    translate(v = [125.50, 6.00, 9.00])
    {
        union()
        {
            union()
            {
                union()
                {
                    union()
                    {
                        cube(size = [27.00, 52.00, 13.00], center = false); 
                        translate(v = [4.00, -6.00, -10.00])
                        {
                            translate(v = [0.00, 4.00, 0.00])
                            {
                                cube(size = [19.00, 14.00, 23.00], center = false); 
                            }
                        }
                    }
                    translate(v = [4.00, 72.00, -10.00])
                    {
                        translate(v = [0.00, -4.00, 0.00])
                        {
                            cube(size = [19.00, 14.00, 23.00], center = false); 
                        }
                    }
                }
                translate(v = [62.50, 13.50, -3.00])
                {
                    rotate([0.00, 0.00, 90.00])
                    {
                        translate(v = [0.00, 0.00, 0.00])
                        {
                            hull()
                            {
                                translate(v = [10.00, 10.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                                }
                                translate(v = [15.00, 10.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                                }
                                translate(v = [0.00, 0.00, 0.00])
                                {
                                    cube(size = [25.00, 1.00, 16.00], center = false); 
                                }
                            }
                        }
                    }
                }
            }
            translate(v = [-105.50, 13.50, -3.00])
            {
                rotate([0.00, 0.00, 90.00])
                {
                    translate(v = [0.00, 0.00, 0.00])
                    {
                        hull()
                        {
                            translate(v = [10.00, 10.00, 0.00])
                            {
                                cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                            }
                            translate(v = [15.00, 10.00, 0.00])
                            {
                                cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                            }
                            translate(v = [0.00, 19.00, 0.00])
                            {
                                cube(size = [25.00, 1.00, 16.00], center = false); 
                            }
                        }
                    }
                }
            }
        }
    }
    translate(v = [51.50, 63.50, 19.50])
    {
        translate(v = [12.50, 12.50, 0.00])
        {
            cylinder(center = false, r1 = 12.5, r2 = 12.5, h = 2.5, $fn = 100);
        }
    }
    translate(v = [21.50, 63.50, 19.50])
    {
        translate(v = [12.50, 12.50, 0.00])
        {
            cylinder(center = false, r1 = 12.5, r2 = 12.5, h = 2.5, $fn = 100);
        }
    }
    translate(v = [111.50, 63.50, 19.50])
    {
        translate(v = [12.50, 12.50, 0.00])
        {
            cylinder(center = false, r1 = 12.5, r2 = 12.5, h = 2.5, $fn = 100);
        }
    }
    translate(v = [141.50, 63.50, 19.50])
    {
        translate(v = [12.50, 12.50, 0.00])
        {
            cylinder(center = false, r1 = 12.5, r2 = 12.5, h = 2.5, $fn = 100);
        }
    }
    translate(v = [14.00, 68.50, 18.00])
    {
        cube(size = [160.00, 15.00, 4.00], center = false); 
    }
    translate(v = [155.50, 6.00, 9.00])
    {
        union()
        {
            union()
            {
                union()
                {
                    union()
                    {
                        cube(size = [27.00, 52.00, 13.00], center = false); 
                        translate(v = [4.00, -6.00, -10.00])
                        {
                            translate(v = [0.00, 4.00, 0.00])
                            {
                                cube(size = [19.00, 14.00, 23.00], center = false); 
                            }
                        }
                    }
                    translate(v = [4.00, 72.00, -10.00])
                    {
                        translate(v = [0.00, -4.00, 0.00])
                        {
                            cube(size = [19.00, 14.00, 23.00], center = false); 
                        }
                    }
                }
                translate(v = [32.50, 13.50, -3.00])
                {
                    rotate([0.00, 0.00, 90.00])
                    {
                        translate(v = [0.00, 0.00, 0.00])
                        {
                            hull()
                            {
                                translate(v = [10.00, 10.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                                }
                                translate(v = [15.00, 10.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                                }
                                translate(v = [0.00, 0.00, 0.00])
                                {
                                    cube(size = [25.00, 1.00, 16.00], center = false); 
                                }
                            }
                        }
                    }
                }
            }
            translate(v = [-135.50, 13.50, -3.00])
            {
                rotate([0.00, 0.00, 90.00])
                {
                    translate(v = [0.00, 0.00, 0.00])
                    {
                        hull()
                        {
                            translate(v = [10.00, 10.00, 0.00])
                            {
                                cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                            }
                            translate(v = [15.00, 10.00, 0.00])
                            {
                                cylinder(center = false, r1 = 10, r2 = 10, h = 16, $fn = 50);
                            }
                            translate(v = [0.00, 19.00, 0.00])
                            {
                                cube(size = [25.00, 1.00, 16.00], center = false); 
                            }
                        }
                    }
                }
            }
        }
    }
}

}
translate([0,-100,22])
{
difference()
{
    translate(v = [4.00, 4.00, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 4, r2 = 4, h = 17);
            translate(v = [180.00, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 17, $fn = 50);
            }
            translate(v = [0.00, 84.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 17, $fn = 50);
            }
            translate(v = [180.00, 84.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 17, $fn = 50);
            }
        }
    }
    translate(v = [29.00, 1.00, 1.00])
    {
        union()
        {
            union()
            {
                cube(size = [130.00, 90.00, 16.00], center = false); 
                translate(v = [159.00, 20.00, -50.00])
                {
                    rotate([0.00, 0.00, 90.00])
                    {
                        translate(v = [0.00, 0.00, 0.00])
                        {
                            hull()
                            {
                                translate(v = [4.00, 46.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 4, r2 = 4, h = 66, $fn = 50);
                                }
                                translate(v = [46.00, 46.00, 0.00])
                                {
                                    cylinder(center = false, r1 = 4, r2 = 4, h = 66, $fn = 50);
                                }
                                translate(v = [0.00, 0.00, 0.00])
                                {
                                    cube(size = [50.00, 1.00, 66.00], center = false); 
                                }
                            }
                        }
                    }
                }
            }
            translate(v = [21.00, 20.00, -50.00])
            {
                rotate([0.00, 0.00, 90.00])
                {
                    translate(v = [0.00, 0.00, 0.00])
                    {
                        hull()
                        {
                            translate(v = [4.00, 4.00, 0.00])
                            {
                                cylinder(center = false, r1 = 4, r2 = 4, h = 66, $fn = 50);
                            }
                            translate(v = [46.00, 4.00, 0.00])
                            {
                                cylinder(center = false, r1 = 4, r2 = 4, h = 66, $fn = 50);
                            }
                            translate(v = [0.00, 49.00, 0.00])
                            {
                                cube(size = [50.00, 1.00, 66.00], center = false); 
                            }
                        }
                    }
                }
            }
        }
    }
    translate(v = [161.00, 2.00, 14.50])
    {
        translate(v = [12.50, 12.50, 0.00])
        {
            cylinder(center = false, r1 = 12.5, r2 = 12.5, h = 2.5, $fn = 100);
        }
    }
    translate(v = [161.00, 65.00, 14.50])
    {
        translate(v = [12.50, 12.50, 0.00])
        {
            cylinder(center = false, r1 = 12.5, r2 = 12.5, h = 2.5, $fn = 100);
        }
    }
}

}
translate([0,-100,39])
{
difference()
{
    translate(v = [4.00, 4.00, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 4, r2 = 4, h = 30.200000000000003);
            translate(v = [180.00, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 30.200000000000003, $fn = 50);
            }
            translate(v = [0.00, 84.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 30.200000000000003, $fn = 50);
            }
            translate(v = [180.00, 84.00, 0.00])
            {
                cylinder(center = false, r1 = 4, r2 = 4, h = 30.200000000000003, $fn = 50);
            }
        }
    }
    translate(v = [2.00, 47.00, 1.20])
    {
        union()
        {
            translate(v = [0.00, 0.00, 7.25])
            {
                cube(size = [40.00, 43.00, 21.75], center = false); 
            }
            translate(v = [20.00, 0.00, 7.25])
            {
                scale(v = [20.00, 43.00, 7.25])
                {
                    rotate([-90.00, 0.00, 0.00])
                    {
                        cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                    }
                }
            }
        }
    }
    translate(v = [46.00, 47.00, 1.20])
    {
        cube(size = [140.00, 43.00, 29.00], center = false); 
    }
    translate(v = [46.00, 2.00, 1.20])
    {
        cube(size = [140.00, 43.00, 29.00], center = false); 
    }
    translate(v = [2.00, 2.00, 1.20])
    {
        union()
        {
            translate(v = [0.00, 0.00, 7.25])
            {
                cube(size = [40.00, 43.00, 21.75], center = false); 
            }
            translate(v = [20.00, 0.00, 7.25])
            {
                scale(v = [20.00, 43.00, 7.25])
                {
                    rotate([-90.00, 0.00, 0.00])
                    {
                        cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                    }
                }
            }
        }
    }
}

}
translate([50,-98,40.2])
{
difference()
{
    cube(size = [138.00, 41.00, 28.00], center = false); 
    translate(v = [1.00, 1.00, 1.20])
    {
        union()
        {
            union()
            {
                translate(v = [0.00, 0.00, 25.00])
                {
                    cube(size = [32.50, 39.00, 75.00], center = false); 
                }
                translate(v = [16.25, 0.00, 25.00])
                {
                    scale(v = [16.25, 39.00, 25.00])
                    {
                        rotate([-90.00, 0.00, 0.00])
                        {
                            cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                        }
                    }
                }
            }
            translate(v = [34.50, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 25.00])
                    {
                        cube(size = [32.50, 39.00, 75.00], center = false); 
                    }
                    translate(v = [16.25, 0.00, 25.00])
                    {
                        scale(v = [16.25, 39.00, 25.00])
                        {
                            rotate([-90.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                            }
                        }
                    }
                }
            }
            translate(v = [69.00, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 25.00])
                    {
                        cube(size = [32.50, 39.00, 75.00], center = false); 
                    }
                    translate(v = [16.25, 0.00, 25.00])
                    {
                        scale(v = [16.25, 39.00, 25.00])
                        {
                            rotate([-90.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                            }
                        }
                    }
                }
            }
            translate(v = [103.50, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 25.00])
                    {
                        cube(size = [32.50, 39.00, 75.00], center = false); 
                    }
                    translate(v = [16.25, 0.00, 25.00])
                    {
                        scale(v = [16.25, 39.00, 25.00])
                        {
                            rotate([-90.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                            }
                        }
                    }
                }
            }
        }
    }
}

}
translate([50,-45,40.2])
{
difference()
{
    cube(size = [138.00, 41.00, 28.00], center = false); 
    translate(v = [1.00, 1.00, 1.20])
    {
        union()
        {
            union()
            {
                translate(v = [0.00, 0.00, 25.00])
                {
                    cube(size = [44.00, 39.00, 75.00], center = false); 
                }
                translate(v = [22.00, 0.00, 25.00])
                {
                    scale(v = [22.00, 39.00, 25.00])
                    {
                        rotate([-90.00, 0.00, 0.00])
                        {
                            cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                        }
                    }
                }
            }
            translate(v = [46.00, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 25.00])
                    {
                        cube(size = [44.00, 39.00, 75.00], center = false); 
                    }
                    translate(v = [22.00, 0.00, 25.00])
                    {
                        scale(v = [22.00, 39.00, 25.00])
                        {
                            rotate([-90.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                            }
                        }
                    }
                }
            }
            translate(v = [92.00, 0.00, 0.00])
            {
                union()
                {
                    translate(v = [0.00, 0.00, 25.00])
                    {
                        cube(size = [44.00, 39.00, 75.00], center = false); 
                    }
                    translate(v = [22.00, 0.00, 25.00])
                    {
                        scale(v = [22.00, 39.00, 25.00])
                        {
                            rotate([-90.00, 0.00, 0.00])
                            {
                                cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                            }
                        }
                    }
                }
            }
        }
    }
}

}
