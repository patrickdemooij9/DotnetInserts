$fn=48;
translate([0,0,0])
{
difference()
{
    cube(size = [177.00, 245.00, 30.00], center = false); 
    translate(v = [124.00, 2.00, 9.00])
    {
        cube(size = [51.00, 241.00, 21.00], center = false); 
    }
    translate(v = [135.00, 29.50, 1.00])
    {
        cube(size = [29.00, 186.00, 2.00], center = false); 
    }
    translate(v = [131.50, 29.50, 3.00])
    {
        cube(size = [36.00, 186.00, 6.00], center = false); 
    }
    translate(v = [126.50, 40.50, 7.00])
    {
        cube(size = [46.00, 164.00, 2.00], center = false); 
    }
    translate(v = [13.00, 2.00, 20.00])
    {
        cube(size = [89.00, 59.00, 6.00], center = false); 
    }
    translate(v = [2.00, 2.00, 26.00])
    {
        union()
        {
            union()
            {
                cube(size = [114.00, 68.00, 4.00], center = false); 
                translate(v = [42.00, 183.00, -30.00])
                {
                    translate(v = [-18.00, 0.00, 0.00])
                    {
                        hull()
                        {
                            translate(v = [5.00, 5.00, 0.00])
                            {
                                cylinder(center = false, r1 = 5, r2 = 5, h = 34, $fn = 50);
                            }
                            translate(v = [25.00, 5.00, 0.00])
                            {
                                cylinder(center = false, r1 = 5, r2 = 5, h = 34, $fn = 50);
                            }
                            translate(v = [0.00, 59.00, 0.00])
                            {
                                cube(size = [30.00, 1.00, 34.00], center = false); 
                            }
                        }
                    }
                }
            }
            translate(v = [175.00, 14.00, -50.00])
            {
                rotate([0.00, 0.00, 90.00])
                {
                    translate(v = [0.00, 0.00, 0.00])
                    {
                        hull()
                        {
                            translate(v = [5.00, 55.00, 0.00])
                            {
                                cylinder(center = false, r1 = 5, r2 = 5, h = 54, $fn = 50);
                            }
                            translate(v = [35.00, 55.00, 0.00])
                            {
                                cylinder(center = false, r1 = 5, r2 = 5, h = 54, $fn = 50);
                            }
                            translate(v = [0.00, 0.00, 0.00])
                            {
                                cube(size = [40.00, 1.00, 54.00], center = false); 
                            }
                        }
                    }
                }
            }
        }
    }
    translate(v = [2.00, 72.50, 28.00])
    {
        cube(size = [81.00, 81.00, 2.00], center = false); 
    }
    translate(v = [85.00, 72.00, 4.00])
    {
        cube(size = [37.00, 171.00, 26.00], center = false); 
    }
    translate(v = [8.00, 170.00, 13.00])
    {
        cube(size = [17.00, 66.00, 17.00], center = false); 
    }
    translate(v = [32.00, 170.00, 13.00])
    {
        cube(size = [17.00, 66.00, 17.00], center = false); 
    }
    translate(v = [2.00, 157.00, 22.00])
    {
        cube(size = [81.00, 86.00, 8.00], center = false); 
    }
    translate(v = [60.00, 196.00, 13.00])
    {
        cube(size = [17.00, 17.00, 17.00], center = false); 
    }
    translate(v = [15.00, 88.50, 20.00])
    {
        translate(v = [26.00, 26.00, 0.00])
        {
            cylinder(center = false, r1 = 26, r2 = 26, h = 2, $fn = 100);
        }
    }
    translate(v = [11.00, 84.50, 22.00])
    {
        translate(v = [30.00, 30.00, 0.00])
        {
            cylinder(center = false, r1 = 30, r2 = 30, h = 6, $fn = 100);
        }
    }
    translate(v = [21.50, 22.00, 10.00])
    {
        cube(size = [40.00, 22.00, 10.00], center = false); 
    }
}

}
translate([86,72,6])
{
difference()
{
    translate(v = [2.00, 2.00, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 2, r2 = 2, h = 12);
            translate(v = [32.00, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 12, $fn = 50);
            }
            translate(v = [0.00, 166.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 12, $fn = 50);
            }
            translate(v = [32.00, 166.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 12, $fn = 50);
            }
        }
    }
    translate(v = [2.00, 2.00, 2.00])
    {
        union()
        {
            translate(v = [0.00, 0.00, 2.50])
            {
                cube(size = [32.00, 82.00, 7.50], center = false); 
            }
            translate(v = [16.00, 0.00, 2.50])
            {
                scale(v = [16.00, 82.00, 2.50])
                {
                    rotate([-90.00, 0.00, 0.00])
                    {
                        cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                    }
                }
            }
        }
    }
    translate(v = [2.00, 86.00, 2.00])
    {
        union()
        {
            translate(v = [0.00, 0.00, 2.50])
            {
                cube(size = [32.00, 82.00, 7.50], center = false); 
            }
            translate(v = [16.00, 0.00, 2.50])
            {
                scale(v = [16.00, 82.00, 2.50])
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
translate([86,72,18])
{
difference()
{
    translate(v = [2.00, 2.00, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 2, r2 = 2, h = 12);
            translate(v = [32.00, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 12, $fn = 50);
            }
            translate(v = [0.00, 166.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 12, $fn = 50);
            }
            translate(v = [32.00, 166.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 12, $fn = 50);
            }
        }
    }
    translate(v = [2.00, 2.00, 2.00])
    {
        union()
        {
            translate(v = [0.00, 0.00, 2.50])
            {
                cube(size = [32.00, 31.00, 7.50], center = false); 
            }
            translate(v = [16.00, 0.00, 2.50])
            {
                scale(v = [16.00, 31.00, 2.50])
                {
                    rotate([-90.00, 0.00, 0.00])
                    {
                        cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                    }
                }
            }
        }
    }
    translate(v = [2.00, 36.00, 2.00])
    {
        union()
        {
            translate(v = [0.00, 0.00, 2.50])
            {
                cube(size = [32.00, 29.00, 7.50], center = false); 
            }
            translate(v = [16.00, 0.00, 2.50])
            {
                scale(v = [16.00, 29.00, 2.50])
                {
                    rotate([-90.00, 0.00, 0.00])
                    {
                        cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                    }
                }
            }
        }
    }
    translate(v = [2.00, 68.50, 2.00])
    {
        union()
        {
            translate(v = [0.00, 0.00, 2.50])
            {
                cube(size = [32.00, 33.00, 7.50], center = false); 
            }
            translate(v = [16.00, 0.00, 2.50])
            {
                scale(v = [16.00, 33.00, 2.50])
                {
                    rotate([-90.00, 0.00, 0.00])
                    {
                        cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                    }
                }
            }
        }
    }
    translate(v = [2.00, 105.00, 2.00])
    {
        union()
        {
            translate(v = [0.00, 0.00, 2.50])
            {
                cube(size = [32.00, 29.00, 7.50], center = false); 
            }
            translate(v = [16.00, 0.00, 2.50])
            {
                scale(v = [16.00, 29.00, 2.50])
                {
                    rotate([-90.00, 0.00, 0.00])
                    {
                        cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                    }
                }
            }
        }
    }
    translate(v = [2.00, 137.00, 2.00])
    {
        union()
        {
            translate(v = [0.00, 0.00, 2.50])
            {
                cube(size = [32.00, 31.00, 7.50], center = false); 
            }
            translate(v = [16.00, 0.00, 2.50])
            {
                scale(v = [16.00, 31.00, 2.50])
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
