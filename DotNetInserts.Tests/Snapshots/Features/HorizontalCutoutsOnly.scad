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
                        translate(v = [32.00, -3.00, -20.00])
                        {
                            translate(v = [0.00, 0.00, 0.00])
                            {
                                cube(size = [20.00, 30.00, 46.00], center = false); 
                            }
                        }
                    }
                    translate(v = [32.00, 57.00, -20.00])
                    {
                        translate(v = [0.00, 0.00, 0.00])
                        {
                            cube(size = [20.00, 30.00, 46.00], center = false); 
                        }
                    }
                }
                translate(v = [87.00, 32.00, -20.00])
                {
                    rotate([0.00, 0.00, 90.00])
                    {
                        translate(v = [0.00, 0.00, 0.00])
                        {
                            cube(size = [20.00, 30.00, 46.00], center = false); 
                        }
                    }
                }
            }
            translate(v = [27.00, 32.00, -20.00])
            {
                rotate([0.00, 0.00, 90.00])
                {
                    translate(v = [0.00, 0.00, 0.00])
                    {
                        cube(size = [20.00, 30.00, 46.00], center = false); 
                    }
                }
            }
        }
    }
}

}
